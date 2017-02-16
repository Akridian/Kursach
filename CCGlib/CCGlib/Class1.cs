using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CCGlib
{
    public class Communications
    {
        void Send(TcpClient receiver, string message)
        {
            NetworkStream stream = receiver.GetStream();
            byte[] buff = Encoding.ASCII.GetBytes(message);
            stream.Write(BitConverter.GetBytes(buff.Length), 0, 4);
            stream.Write(buff, 0, message.Length);
        }

        string Receive(TcpClient sender)
        {
            NetworkStream stream = sender.GetStream();
            byte[] buff = new byte[4];
            stream.Read(buff, 0, 4);
            int length = BitConverter.ToInt32(buff, 0);
            buff = new byte[length];
            stream.Read(buff, 0, length);
            return Encoding.ASCII.GetString(buff);
        }
    }

    public class Game
    {
        public enum Hero { None, Engineer, Knight, Necromancer, Alchemist, Mage }
        public class Card
        {
            int manacost;
            int hp;
            int attack;
            string name;
            string abilityText;
        }

        public class BoardCard : Card
        {
            int currentHP;
            int currentAttack;
            bool canAttack;
        }

        public class Player
        {
            int hp;
            int mana;
            bool active;
            bool heroAbility;
            List<Card> hand;
            List<Card> deck;
            List<BoardCard> board;
            TcpClient client;
            Hero hero;

            public Player(TcpClient cl)
            {
                hand = new List<Card>();
                deck = new List<Card>();
                board = new List<BoardCard>();
                hp = 30;
                mana = 1;
                heroAbility = true;
                client = cl;
            }
        }

        Player player1;
        Player player2;

        public Game(TcpClient cl1, TcpClient cl2)
        {
            player1 = new Player(cl1);
            player2 = new Player(cl2);
        }
    }
}
