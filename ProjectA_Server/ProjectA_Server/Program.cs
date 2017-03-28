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
        static void Main(string[] args)
        {
            Queue<TcpClient> queue = new Queue<TcpClient>();
            TcpListener server = new TcpListener(IPAddress.Any, 4444);
            server.Start();
            TcpClient connection;
            while (true)
            {
                connection = server.AcceptTcpClient();
                queue.Enqueue(connection);
                if (queue.Count>1)
                {
                    new Thread(new ParameterizedThreadStart(Game)).Start(new Tuple<TcpClient, TcpClient>(queue.Dequeue(), queue.Dequeue()));
                }
            }
        }

        static void Game(object o)
        {
            TcpClient player1 = (o as Tuple<TcpClient, TcpClient>).Item1;
            TcpClient player2 = (o as Tuple<TcpClient, TcpClient>).Item2;
            Send(player1, "1:1:2:3:2:1:3:3");
            Send(player2, "1:1:2:3:2:1:3:3");
            player1.Close();
            player2.Close();
        }
    }
}
