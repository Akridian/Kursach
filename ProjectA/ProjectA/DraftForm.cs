using CardControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProjectA
{
    public partial class DraftForm : Form
    {
        List<int> Deck = new List<int>();
        public DraftForm()
        {
            InitializeComponent();
            ChangeCards();
        }

        public List<int> TakeThree()
        {
            List<int> result = new List<int>();
            while (result.Count < 3)
            {
                if (Deck.Count < 4)
                {
                    int cardCount = Card.Common.Count + Card.Rare.Count + Card.Epic.Count + Card.Legendary.Count;
                    double r = new Random().NextDouble();
                    if (r < 0.5)
                    {
                        int i = new Random().Next(0, Card.Common.Count);
                        if (!result.Contains(Card.Common[i]))
                        {
                            result.Add(Card.Common[i]);
                        }
                    }
                    else if (r < 0.8)
                    {
                        int i = new Random().Next(0, Card.Rare.Count);
                        if (!result.Contains(Card.Rare[i]))
                        {
                            result.Add(Card.Rare[i]);
                        }
                    }
                    else if (r < 0.95)
                    {
                        int i = new Random().Next(0, Card.Epic.Count);
                        if (!result.Contains(Card.Epic[i]))
                        {
                            result.Add(Card.Epic[i]);
                        }
                    }
                    else
                    {
                        int i = new Random().Next(0, Card.Legendary.Count);
                        if (!result.Contains(Card.Legendary[i]))
                        {
                            result.Add(Card.Legendary[i]);
                        }
                    }
                }
                else if (Deck.Count < 7)
                {
                    int cardCount = Card.Rare.Count + Card.Epic.Count + Card.Legendary.Count;
                    double r = new Random().NextDouble();
                    if (r < 0.6)
                    {
                        int i = new Random().Next(0, Card.Rare.Count);
                        if (!result.Contains(Card.Rare[i]))
                        {
                            result.Add(Card.Rare[i]);
                        }
                    }
                    else if (r < 0.9)
                    {
                        int i = new Random().Next(0, Card.Epic.Count);
                        if (!result.Contains(Card.Epic[i]))
                        {
                            result.Add(Card.Epic[i]);
                        }
                    }
                    else
                    {
                        int i = new Random().Next(0, Card.Legendary.Count);
                        if (!result.Contains(Card.Legendary[i]))
                        {
                            result.Add(Card.Legendary[i]);
                        }
                    }
                }
                else
                {
                    int cardCount = Card.Epic.Count + Card.Legendary.Count;
                    double r = new Random().NextDouble();
                    if (r < 0.75)
                    {
                        int i = new Random().Next(0, Card.Epic.Count);
                        if (!result.Contains(Card.Epic[i]))
                        {
                            result.Add(Card.Epic[i]);
                        }
                    }
                    else
                    {
                        int i = new Random().Next(0, Card.Legendary.Count);
                        if (!result.Contains(Card.Legendary[i]))
                        {
                            result.Add(Card.Legendary[i]);
                        }
                    }
                }
            }
            return result;
        }
        public void Card_SelectionChanged(object sender, EventArgs e)
        {
            Card card = sender as Card;
            if (card.Selected)
            {
                foreach (Card c in cardsPanel.Controls)
                {
                    if (c.Selected && (c != card))
                    {
                        c.Selected = false;
                    }
                }
            }
        }

        public void ChangeCards()
        {
            List<int> cards = TakeThree();

            Card card1 = new Card(cards[0], true);
            Card card2 = new Card(cards[1], true);
            Card card3 = new Card(cards[2], true);

            card1.SelectionChangedEvent += Card_SelectionChanged;
            card2.SelectionChangedEvent += Card_SelectionChanged;
            card3.SelectionChangedEvent += Card_SelectionChanged;

            card1.Location = new Point(148, 0);
            card2.Location = new Point(349, 0);
            card3.Location = new Point(550, 0);

            cardsPanel.Controls.Clear();

            cardsPanel.Controls.Add(card1);
            cardsPanel.Controls.Add(card2);
            cardsPanel.Controls.Add(card3);
        }
        private void DraftForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Environment.Exit(0);
            }
        }
        private void takeButton_Click(object sender, EventArgs e)
        {
            Card selectedCard = new Card();
            foreach (Card card in cardsPanel.Controls)
            {
                if (card.Selected)
                {
                    selectedCard = card;
                }
            }
            if (selectedCard.ID == 0)
            {
                MessageBox.Show("Выберите карту!");
            }
            else
            {
                Deck.Add(selectedCard.ID);
                if (Deck.Count == 8)
                {
                    TcpClient server = new TcpClient();
                    try
                    {
                        server.Connect("localhost", 44444);
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось подключиться к серверу");
                        Environment.Exit(1);
                    }
                    string msg = "";
                    foreach (int card in Deck)
                    {
                        msg += card + "," + new Card(card, false).Power + ":";
                    }
                    if (msg != "")
                    {
                        msg = msg.Substring(0, msg.Length - 1);
                    }
                    Send(server, msg);
                    Hide();
                    Form wait = new WaitForm();
                    wait.Show();
                    new Thread(new ThreadStart(new Action(() =>
                    {
                        msg = Receive(server);
                        Invoke(new Action(() =>
                        {
                            wait.Close();
                            new GameForm(server, msg).Show();
                        }));
                    }))).Start();
                }
                else
                {
                    ChangeCards();
                }
            }
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
                MessageBox.Show("Не удалось передать данные на сервер.");
                Environment.Exit(2);
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
                MessageBox.Show("Соединение с сервером потеряно.");
                Environment.Exit(3);
            }
            string msg = Encoding.ASCII.GetString(buff);
            msg = msg.Substring(0, msg.Length - 1);
            return msg;
        }
    }
}
