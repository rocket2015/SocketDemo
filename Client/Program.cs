using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketClent client = new SocketClent();
            if(client.Connect())
                client.SendMessage();
        }
    }

    class SocketClent
    {
        Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

        byte[] GenRandomBytes()
        {
            Random ran = new Random();
            char[] buffer = new char[100];
            int len = ran.Next(100);
            for (int k = 0; k < len; k++)
            {
                int index = ran.Next(62);
                if (index < 10)
                    buffer[k] = (char)((int)'0' + index);
                else if (index < 36)
                    buffer[k] = (char)((int)'A' + index - 10);
                else if (index < 62)
                    buffer[k] = (char)((int)'a' + index - 36);
            }

            string str = new string(buffer, 0, len);
            var bytes = Encoding.Default.GetBytes(str);
            return bytes;
        }

        public bool Connect()
        {
            try
            {
                socket.Connect("127.0.0.1", 10000);
                return true;
            }
            catch(SocketException se)
            {
                Console.WriteLine("Connect Error:{0}",se.Message);
                return false;
            }
        }

        public void SendMessage()
        {
            for (int i = 0; i < 10; i++)
            {
                var bytes = GenRandomBytes();
                socket.Send(bytes);
                Thread.Sleep(1000);
            }
        }
    }
}
