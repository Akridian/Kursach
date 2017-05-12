using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                Console.WriteLine("Не удалось передать данные клиенту. Завершаю игру.");
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
                Console.WriteLine("Не удалось получить данные. Завершаю игру.");
                RemoveClients(sender, true);
                Thread.CurrentThread.Abort();
            }
            string msg = Encoding.ASCII.GetString(buff);
            msg = msg.Substring(0, msg.Length - 1);
            return msg;
        }

        static void RemoveClients (TcpClient client, bool withError)
        {
            foreach (Tuple<TcpClient, TcpClient> pair in clients)
            {
                if (pair.Item1 == client)
                {
                    clients.Remove(pair);
                    if (withError)
                    {
                        Send(pair.Item2, "error");
                        Console.WriteLine("Послал ошибку.");
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
                        Console.WriteLine("Послал ошибку.");
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
                    new Thread(new ParameterizedThreadStart(Game)).Start(pair);
                }
            }
        }

        static void Game(object o)
        {
            bool eog = false;
            Player player1 = new Player();
            player1.Client = (o as Tuple<TcpClient, TcpClient>).Item1;
            Player player2 = new Player();
            player2.Client = (o as Tuple<TcpClient, TcpClient>).Item2;
            player1.Hand = Receive(player1.Client);
            Console.WriteLine("Получил первую руку");
            player2.Hand = Receive(player2.Client);
            Console.WriteLine("Получил вторую руку");
            Player activePlayer = player1;
            Player passivePlayer = player2;
            Send(activePlayer.Client, activePlayer.Hand);
            Console.WriteLine("Послал первую руку");
            Send(passivePlayer.Client, passivePlayer.Hand);
            Console.WriteLine("Послал вторую руку");
            while ((activePlayer.Rounds != 2) && (passivePlayer.Rounds != 2))
            {
                while (!eog)
                {
                    Send(activePlayer.Client, "game");
                    Console.WriteLine("Посылаю игру");
                    Send(activePlayer.Client, passivePlayer.Range);
                    Console.WriteLine("Послал вражей реньжей");
                    Send(activePlayer.Client, passivePlayer.Melee);
                    Console.WriteLine("Послал вражей мили");
                    Send(activePlayer.Client, activePlayer.Melee);
                    Console.WriteLine("Послал твоих мили");
                    Send(activePlayer.Client, activePlayer.Range);
                    Console.WriteLine("Послал твоих реньжей");
                    Send(activePlayer.Client, activePlayer.Hand);
                    Console.WriteLine("Послал руку");
                    string msg = Receive(activePlayer.Client);
                    if (msg == "spy")
                    {
                        string card = "";
                        if (passivePlayer.Hand.Length != 0)
                        {
                            string[] cards = passivePlayer.Hand.Split(':');
                            card = cards[new Random().Next(0, cards.Length)];
                        }
                        Send(activePlayer.Client, "spy");
                        Console.WriteLine("Послал карту с шпиона");
                        Send(activePlayer.Client, card);
                        msg = Receive(activePlayer.Client);
                    }
                    if (msg == "game")
                    {
                        passivePlayer.Range = Receive(activePlayer.Client);
                        Console.WriteLine("Получил вражей реньжей\n" + passivePlayer.Range);
                        passivePlayer.Melee = Receive(activePlayer.Client);
                        Console.WriteLine("Получил вражей мили\n" + passivePlayer.Melee);
                        activePlayer.Melee = Receive(activePlayer.Client);
                        Console.WriteLine("Получил твоих мили\n" + activePlayer.Melee);
                        activePlayer.Range = Receive(activePlayer.Client);
                        Console.WriteLine("Получил твоих реньжей\n" + activePlayer.Range);
                        activePlayer.Hand = Receive(activePlayer.Client);
                        Console.WriteLine("Получил руку\n" + activePlayer.Hand);
                        if (activePlayer.Hand == "")
                        {
                            activePlayer.Pass = true;
                        }
                    }
                    else if (msg == "pass")
                    {
                        activePlayer.Pass = true;
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
                    }
                    if (activePlayer.Pass && passivePlayer.Pass)
                    {
                        eog = true;
                    }
                }
                Send(passivePlayer.Client, "game");
                Console.WriteLine("Посылаю игру");
                Send(passivePlayer.Client, activePlayer.Range);
                Console.WriteLine("Послал вражей реньжей");
                Send(passivePlayer.Client, activePlayer.Melee);
                Console.WriteLine("Послал вражей мили");
                Send(passivePlayer.Client, passivePlayer.Melee);
                Console.WriteLine("Послал твоих мили");
                Send(passivePlayer.Client, passivePlayer.Range);
                Console.WriteLine("Послал твоих реньжей");
                Send(passivePlayer.Client, passivePlayer.Hand);
                Console.WriteLine("Послал руку");

                Send(activePlayer.Client, "score");
                Console.WriteLine("Запросил счет 1");
                activePlayer.Score = Receive(activePlayer.Client);
                Console.WriteLine("Получил счет 1\n" + activePlayer.Score);
                Send(passivePlayer.Client, "score");
                Console.WriteLine("Запросил счет 2");
                passivePlayer.Score = Receive(passivePlayer.Client);
                Console.WriteLine("Получил счет 2\n" + passivePlayer.Score);
                if (activePlayer.Score == passivePlayer.Score)
                {
                    Send(activePlayer.Client, "round_draw");
                    activePlayer.Rounds++;
                    Send(passivePlayer.Client, "round_draw");
                    passivePlayer.Rounds++;
                }
                else
                {
                    if (int.Parse(activePlayer.Score) > int.Parse(passivePlayer.Score))
                    {
                        Send(activePlayer.Client, "round_victory");
                        activePlayer.Rounds++;
                        Send(passivePlayer.Client, "round_defeat");
                    }
                    else
                    {
                        Send(passivePlayer.Client, "round_victory");
                        passivePlayer.Rounds++;
                        Send(activePlayer.Client, "round_defeat");
                    }
                }
                activePlayer.Melee = "";
                activePlayer.Range = "";
                passivePlayer.Melee = "";
                passivePlayer.Range = "";
                activePlayer.Pass = false;
                passivePlayer.Pass = false;
                eog = false;
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
            }
            if (activePlayer.Rounds == passivePlayer.Rounds)
            {
                Send(activePlayer.Client, "draw");
                Send(passivePlayer.Client, "draw");
            }
            else
            {
                if (activePlayer.Rounds == 2)
                {
                    Send(activePlayer.Client, "victory");
                    Send(passivePlayer.Client, "defeat");
                }
                else
                {
                    Send(passivePlayer.Client, "victory");
                    Send(activePlayer.Client, "defeat");
                }
            }
            RemoveClients(player1.Client, false);
        }
    }
}
