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

namespace ProjectA
{
    public partial class Form1 : Form
    {
        static void Send(TcpClient receiver, string message)
        {
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
            return Encoding.ASCII.GetString(buff);
        }

        TcpClient client;
        public Form1()
        {
            InitializeComponent();
            try
            {
                client = new TcpClient();
                client.Connect("localhost", 4444);
                string msg = Receive(client);
                string[] s = msg.Split(':');
                foreach (string id in s)
                {
                    hand.Controls.Add(new Card(int.Parse(id), true));
                }
                foreach (Card c in hand.Controls)
                {
                    c.SelectionChangedEvent += Card_SelectionChanged;
                }
            }
            catch
            {

            }
            Redraw();
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
                }
            }
            Redraw();
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
    }
}
