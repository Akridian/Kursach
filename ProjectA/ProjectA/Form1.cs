using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardControl;
using System.Net.Sockets;
using System.Threading;

namespace ProjectA
{
    public partial class Form1 : Form
    {
        static void Send(TcpClient receiver, string message)
        {
            message += "?";
            NetworkStream stream = receiver.GetStream();
            byte[] buff = Encoding.ASCII.GetBytes(message);
            stream.Write(BitConverter.GetBytes(buff.Length), 0, 4);
            stream.Write(buff, 0, message.Length);
        }

        static string Receive(TcpClient sender)
        {
            NetworkStream stream = sender.GetStream();
            byte[] buff = new byte[4];
            stream.Read(buff, 0, 4);
            int length = BitConverter.ToInt32(buff, 0);
            buff = new byte[length];
            stream.Read(buff, 0, length);
            string msg = Encoding.ASCII.GetString(buff);
            msg = msg.Substring(0, msg.Length - 1);
            return msg;
        }

        TcpClient server;
        public Form1()
        {
            InitializeComponent();
            Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool boolka = true;
            string msg = "";
            try
            {
                server = new TcpClient();
                server.Connect("localhost", 44444);
                msg = Receive(server);
            }
            catch
            {
                msg = "1:1:2:2:3:3";
                boolka = false;
                playCard.Enabled = true;
            }
            string[] s = msg.Split(':');
            foreach (string id in s)
            {
                hand.Controls.Add(new Card(int.Parse(id), true));
            }
            foreach (Card c in hand.Controls)
            {
                c.SelectionChangedEvent += Card_SelectionChanged;
            }
            Redraw();
            Visible = true;
            if (boolka)
            {
                new Thread(new ThreadStart(Game)).Start();
            }
        }

        public void UnpackLine(Control line, string msg, bool inhand)
        {
            if (msg != "")
            {
                Invoke(new Action(() =>
                {
                    line.Controls.Clear();
                    string[] s = msg.Split(':');
                    foreach (string id in s)
                    {
                        line.Controls.Add(new Card(int.Parse(id), inhand));
                    }
                }));
            }
            else
            {
                Invoke(new Action(() =>
                {
                    line.Controls.Clear();
                }));
            }
        }
        public void Game()
        {
            bool eog = false;
            while (!eog)
            {
                try
                {
                    string msg = Receive(server);
                    if (msg == "game")
                    {
                        msg = Receive(server);
                        UnpackLine(enemyRange, msg, false);

                        msg = Receive(server);
                        UnpackLine(enemyMelee, msg, false);

                        msg = Receive(server);
                        UnpackLine(yourMelee, msg, false);

                        msg = Receive(server);
                        UnpackLine(yourRange, msg, false);

                        msg = Receive(server);
                        UnpackLine(hand, msg, true);
                        Invoke(new Action(() =>
                        {
                            foreach (Card c in hand.Controls)
                            {
                                c.SelectionChangedEvent += Card_SelectionChanged;
                            }
                            playCard.Enabled = true;
                        }));
                    }
                    else if (msg == "score")
                    {
                        Send(server, yourScore.Text);
                    }
                    else if (msg == "victory")
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show("VICTORY");
                        }));
                        eog = true;
                    }
                    else if (msg == "draw")
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show("DRAW");
                        }));
                        eog = true;
                    }
                    else if (msg == "defeat")
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show("DEFEAT");
                        }));
                        eog = true;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    eog = true;
                }
                Invoke(new Action(() =>
                {
                    Redraw();
                }));
            }
            Environment.Exit(0);
        }
        public void DrawLine(GroupBox gr)
        {
            if (gr.Controls.Count == 0)
            {
                gr.Text = "0";
            }
            else
            {
                int p = 0;
                int x = -106;
                int y = 16;
                foreach (Control c in gr.Controls)
                {
                    p += (c as Card).Power;
                    x += 112;
                    c.Location = new Point(x, y);
                }
                gr.Text = p.ToString();
            }
        }
        public void Redraw()
        {
            DrawLine(enemyRange);
            DrawLine(enemyMelee);
            DrawLine(yourMelee);
            DrawLine(yourRange);
            DrawLine(hand);
            enemyScore.Text = (int.Parse(enemyRange.Text) + int.Parse(enemyMelee.Text)).ToString();
            yourScore.Text = (int.Parse(yourRange.Text) + int.Parse(yourMelee.Text)).ToString();
        }

        private void playCard_Click(object sender, EventArgs e)
        {
            playCard.Enabled = false;
            for (int i = 0; i<hand.Controls.Count; i++)
            {
                Card c = hand.Controls[i] as Card;
                if (c.Selected)
                {
                    c.Selected = false;
                    hand.Controls.Remove(c);
                    if (c.Type==Card.AttackType.Melee)
                    {
                        yourMelee.Controls.Add(c);
                        i = 0;
                    }
                    else if (c.Type == Card.AttackType.Range)
                    {
                        yourRange.Controls.Add(c);
                        i = 0;
                    }
                    c.InHand = false;
                }
            }
            Redraw();
            new Thread(new ThreadStart(SendLines)).Start();
        }

        public void SendLines()
        {
            try
            {
                Send(server, "game");
                SendLine(enemyRange.Controls);
                SendLine(enemyMelee.Controls);
                SendLine(yourMelee.Controls);
                SendLine(yourRange.Controls);
                SendLine(hand.Controls);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                playCard.Invoke(new Action(() =>
                {
                    playCard.Enabled = true;
                }));
            }
        }
        public void SendLine(Control.ControlCollection controls)
        {
            string msg = "";
            foreach (Control c in controls)
            {
                Card card = c as Card;
                msg += card.Id + ":";
            }
            if (msg != "")
            {
                msg = msg.Substring(0, msg.Length - 1);
            }
            Send(server, msg);
        }
        public void Card_SelectionChanged(object sender, EventArgs e)
        {
            Card c = sender as Card;
            if (c.Selected)
            {
                foreach (Card hc in hand.Controls)
                {
                    if (hc.Selected&&(hc!=c))
                    {
                        hc.Selected = false;
                    }
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            enemyRange.Width = Width - enemyRange.Location.X * 2;
            enemyMelee.Width = Width - enemyMelee.Location.X * 2;
            yourRange.Width = Width - yourRange.Location.X * 2;
            yourMelee.Width = Width - yourMelee.Location.X * 2;
            hand.Width = Width - hand.Location.X * 2;
            playCard.Location = new Point(Width / 2 - playCard.Width / 2, yourRange.Location.Y + yourRange.Height + (hand.Location.Y - (yourRange.Location.Y + yourRange.Height)) / 2-  playCard.Height/2);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Environment.Exit(0);
            }
        }

    }
}
