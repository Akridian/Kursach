using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using NLog.Targets;
using NLog.Config;

namespace ProjectA_Server
{
    class Program
    {
        class Player
        {
            public TcpClient Client;
            public string Hand = "";
            public string Melee = "";
            public string Range = "";
            public bool Pass = false;
            public string Score = "";
            public int Rounds = 0;
        }
        static void Send(TcpClient receiver, string message)
        {
            message += "?";
            try
            {
                NetworkStream stream = receiver.GetStream();
                byte[] buff = Encoding.ASCII.GetBytes(message);
                stream.Write(BitConverter.GetBytes(buff.Length), 0, 4);
                stream.Write(buff, 0, message.Length);
            }
            catch
            {
                Logger logger = LogManager.GetLogger("Logger" + Thread.CurrentThread.Name);
                logger.Info("Не удалось передать данные клиенту. Завершаю игру.");
                RemoveClients(receiver, true);
                Thread.CurrentThread.Abort();
            }
        }

        static string Receive(TcpClient sender)
        {
            byte[] buff = new byte[4];
            try
            {
                NetworkStream stream = sender.GetStream();
                stream.Read(buff, 0, 4);
                int length = BitConverter.ToInt32(buff, 0);
                buff = new byte[length];
                stream.Read(buff, 0, length);
            }
            catch
            {
                Logger logger = LogManager.GetLogger("Logger" + Thread.CurrentThread.Name);
                logger.Info("Не удалось получить данные. Завершаю игру.");
                RemoveClients(sender, true);
                Thread.CurrentThread.Abort();
            }
            string msg = Encoding.ASCII.GetString(buff);
            msg = msg.Substring(0, msg.Length - 1);
            return msg;
        }

        static void RemoveClients(TcpClient client, bool withError)
        {
            foreach (Tuple<TcpClient, TcpClient> pair in clients)
            {
                if (pair.Item1 == client)
                {
                    clients.Remove(pair);
                    if (withError)
                    {
                        Send(pair.Item2, "error");
                        Logger logger = LogManager.GetLogger("Logger" + Thread.CurrentThread.Name);
                        logger.Info("Послал ошибку.");
                        pair.Item2.Close();
                    }
                    else
                    {
                        pair.Item1.Close();
                        pair.Item2.Close();
                    }
                    break;
                }
                else if (pair.Item2 == client)
                {
                    clients.Remove(pair);
                    if (withError)
                    {
                        Send(pair.Item1, "error");
                        Logger logger = LogManager.GetLogger("Logger" + Thread.CurrentThread.Name);
                        logger.Info("Послал ошибку.");
                        pair.Item1.Close();
                    }
                    else
                    {
                        pair.Item1.Close();
                        pair.Item2.Close();
                    }
                    break;
                }
            }
        }

        static List<Tuple<TcpClient, TcpClient>> clients = new List<Tuple<TcpClient, TcpClient>>();

        static int gameCount = 0;

        static void Main(string[] args)
        {
            List<TcpClient> list = new List<TcpClient>();
            TcpListener server = new TcpListener(IPAddress.Any, 44444);
            server.Start();
            TcpClient connection;
            while (true)
            {
                connection = server.AcceptTcpClient();
                list.Add(connection);
                int i = 0;
                while (i < list.Count)
                {
                    try
                    {
                        list[i].GetStream().Write(new byte[1], 0, 0);
                        i++;
                    }
                    catch
                    {

                        list.RemoveAt(i);
                    }
                }
                if (list.Count > 1)
                {
                    TcpClient first = list[0];
                    TcpClient second = list[1];
                    list.RemoveRange(0, 2);
                    Tuple<TcpClient, TcpClient> pair = new Tuple<TcpClient, TcpClient>(first, second);
                    clients.Add(pair);
                    gameCount++;
                    Tuple<TcpClient, TcpClient, int> parameters = new Tuple<TcpClient, TcpClient, int>(first, second, gameCount);
                    new Thread(new ParameterizedThreadStart(Game)).Start(parameters);
                }
            }
        }

        static void Game(object o)
        {
            int game = (o as Tuple<TcpClient, TcpClient, int>).Item3;
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget fileTarget = new FileTarget();
            fileTarget.FileName = "log_" + DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss") + "_game_" + game + ".txt";
            fileTarget.Layout = "${message}";
            fileTarget.DeleteOldFileOnStartup = true;
            LoggingRule rule = new LoggingRule("*", LogLevel.Info, fileTarget);
            config.LoggingRules.Add(rule);
            LogManager.Configuration = config;
            Logger logger = LogManager.GetLogger("Logger" + game);
            Thread.CurrentThread.Name = game.ToString();

            bool eog = false;
            Player player1 = new Player();
            player1.Client = (o as Tuple<TcpClient, TcpClient, int>).Item1;
            Player player2 = new Player();
            player2.Client = (o as Tuple<TcpClient, TcpClient, int>).Item2;
            player1.Hand = Receive(player1.Client);
            logger.Info("Получил первую руку (" + player1.Hand + ")");
            player2.Hand = Receive(player2.Client);
            logger.Info("Получил вторую руку (" + player2.Hand + ")");
            Player activePlayer = player1;
            Player passivePlayer = player2;
            Send(activePlayer.Client, activePlayer.Hand);
            logger.Info("Послал первую руку (" + activePlayer.Hand + ")");
            Send(passivePlayer.Client, passivePlayer.Hand);
            logger.Info("Послал вторую руку (" + passivePlayer.Hand + ")");
            while ((activePlayer.Rounds != 2) && (passivePlayer.Rounds != 2))
            {
                while (!eog)
                {
                    Send(activePlayer.Client, "game");
                    logger.Info("Посылаю игру");
                    Send(activePlayer.Client, passivePlayer.Range);
                    logger.Info("Послал вражей реньжей (" + passivePlayer.Range + ")");
                    Send(activePlayer.Client, passivePlayer.Melee);
                    logger.Info("Послал вражей мили (" + passivePlayer.Melee + ")");
                    Send(activePlayer.Client, activePlayer.Melee);
                    logger.Info("Послал твоих мили (" + activePlayer.Melee + ")");
                    Send(activePlayer.Client, activePlayer.Range);
                    logger.Info("Послал твоих реньжей (" + activePlayer.Range + ")");
                    Send(activePlayer.Client, activePlayer.Hand);
                    logger.Info("Послал руку (" + activePlayer.Hand + ")");
                    string msg = Receive(activePlayer.Client);
                    if (msg == "spy")
                    {
                        logger.Info("Получил запрос на карту со шпиона");
                        string card = "";
                        if (passivePlayer.Hand.Length != 0)
                        {
                            string[] cards = passivePlayer.Hand.Split(':');
                            card = cards[new Random().Next(0, cards.Length)];
                        }
                        Send(activePlayer.Client, "spy");
                        Send(activePlayer.Client, card);
                        logger.Info("Послал карту с шпиона (" + card + ")");
                        msg = Receive(activePlayer.Client);
                    }
                    if (msg == "game")
                    {
                        logger.Info("Получаю игру от активного");
                        passivePlayer.Range = Receive(activePlayer.Client);
                        logger.Info("Получил вражей реньжей (" + passivePlayer.Range + ")");
                        passivePlayer.Melee = Receive(activePlayer.Client);
                        logger.Info("Получил вражей мили (" + passivePlayer.Melee + ")");
                        activePlayer.Melee = Receive(activePlayer.Client);
                        logger.Info("Получил твоих мили (" + activePlayer.Melee + ")");
                        activePlayer.Range = Receive(activePlayer.Client);
                        logger.Info("Получил твоих реньжей (" + activePlayer.Range + ")");
                        activePlayer.Hand = Receive(activePlayer.Client);
                        logger.Info("Получил руку (" + activePlayer.Hand + ")");
                        if (activePlayer.Hand == "")
                        {
                            activePlayer.Pass = true;
                            logger.Info("У игрока кончились карты - пас");
                        }
                    }
                    else if (msg == "pass")
                    {
                        activePlayer.Pass = true;
                        logger.Info("Игрок спасовал");
                    }
                    if (!passivePlayer.Pass)
                    {
                        if (activePlayer == player1)
                        {
                            activePlayer = player2;
                            passivePlayer = player1;
                        }
                        else
                        {
                            activePlayer = player1;
                            passivePlayer = player2;
                        }
                        logger.Info("Смена активного игрока.");
                    }
                    if (activePlayer.Pass && passivePlayer.Pass)
                    {
                        eog = true;
                        logger.Info("Конец раунда");
                    }
                }
                Send(passivePlayer.Client, "game");
                logger.Info("Посылаю игру пассивному игроку");
                Send(passivePlayer.Client, activePlayer.Range);
                logger.Info("Послал вражей реньжей (" + activePlayer.Range + ")");
                Send(passivePlayer.Client, activePlayer.Melee);
                logger.Info("Послал вражей мили (" + activePlayer.Melee + ")");
                Send(passivePlayer.Client, passivePlayer.Melee);
                logger.Info("Послал твоих мили (" + passivePlayer.Melee + ")");
                Send(passivePlayer.Client, passivePlayer.Range);
                logger.Info("Послал твоих реньжей (" + passivePlayer.Range + ")");
                Send(passivePlayer.Client, passivePlayer.Hand);
                logger.Info("Послал руку (" + passivePlayer.Hand + ")");

                Send(activePlayer.Client, "score");
                logger.Info("Запросил счет активного");
                activePlayer.Score = Receive(activePlayer.Client);
                logger.Info("Получил счет активного (" + activePlayer.Score + ")");
                Send(passivePlayer.Client, "score");
                logger.Info("Запросил счет пассивного");
                passivePlayer.Score = Receive(passivePlayer.Client);
                logger.Info("Получил счет пассивного (" + passivePlayer.Score + ")");
                if (activePlayer.Score == passivePlayer.Score)
                {
                    Send(activePlayer.Client, "round_draw");
                    activePlayer.Rounds++;
                    Send(passivePlayer.Client, "round_draw");
                    passivePlayer.Rounds++;
                    logger.Info("Послал ничью в раунде обоим");
                }
                else
                {
                    if (int.Parse(activePlayer.Score) > int.Parse(passivePlayer.Score))
                    {
                        Send(activePlayer.Client, "round_victory");
                        logger.Info("Послал победу в раунде активному");
                        activePlayer.Rounds++;
                        Send(passivePlayer.Client, "round_defeat");
                        logger.Info("Послал поражение в раунде пассивному");
                    }
                    else
                    {
                        Send(passivePlayer.Client, "round_victory");
                        logger.Info("Послал победу в раунде пассивному");
                        passivePlayer.Rounds++;
                        Send(activePlayer.Client, "round_defeat");
                        logger.Info("Послал поражение в раунде активному");
                    }
                }
                activePlayer.Melee = "";
                activePlayer.Range = "";
                passivePlayer.Melee = "";
                passivePlayer.Range = "";
                activePlayer.Pass = false;
                passivePlayer.Pass = false;
                eog = false;
                logger.Info("Сбросил борду");
                if (activePlayer == player1)
                {
                    activePlayer = player2;
                    passivePlayer = player1;
                }
                else
                {
                    activePlayer = player1;
                    passivePlayer = player2;
                }
                logger.Info("Сменил активного игрока");
            }
            logger.Info("Конец игры");
            if (activePlayer.Rounds == passivePlayer.Rounds)
            {
                Send(activePlayer.Client, "draw");
                Send(passivePlayer.Client, "draw");
                logger.Info("Послал ничью обоим");
            }
            else
            {
                if (activePlayer.Rounds == 2)
                {
                    Send(activePlayer.Client, "victory");
                    logger.Info("Послал победу активному");
                    Send(passivePlayer.Client, "defeat");
                    logger.Info("Послал поражение пассивному");
                }
                else
                {
                    Send(passivePlayer.Client, "victory");
                    logger.Info("Послал победу пассивному");
                    Send(activePlayer.Client, "defeat");
                    logger.Info("Послал поражение активному");
                }
            }
            RemoveClients(player1.Client, false);
        }
    }
}
