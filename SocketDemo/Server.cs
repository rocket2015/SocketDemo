using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SocketDemo
{
    class Server
    {
        private Socket socket;
        private bool isAlive;

        public Server()
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            isAlive = true;
        }

        private void Bind(int port)
        {
            System.Net.EndPoint endPoint = new System.Net.IPEndPoint(IPAddress.Any, port);
            socket.Bind(endPoint);
        }

        public void Start()
        {
            Bind(10000);
            socket.Listen(10);
            Console.WriteLine("Server begin to listen");
            while(isAlive)
            {
                var newSocket = socket.Accept();
                Console.WriteLine("Accept a connect :{0},",newSocket.RemoteEndPoint);
                Thread thread = new Thread(ParseMessage);
                thread.Start(newSocket);
            }
        }

        public void Quit()
        {
            isAlive = false;
            socket.Disconnect(false);
            socket.Close();
        }

        void ParseMessage(object obj)
        {
            var socket = obj as Socket;
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[512];
                    int count = socket.Receive(buffer);
                    Console.WriteLine(count);
                    string msg = Encoding.ASCII.GetString(buffer, 0, count);
                    Console.WriteLine("Receive Data:{0}", msg);
                    var sendMsg = DateTime.Now.ToLongTimeString();
                    var bytes = Encoding.Default.GetBytes(sendMsg);
                    socket.Send(bytes);
                }
            }
            catch(SocketException se)
            {
                Console.WriteLine("ParseMessage Error:{0}", se.Message);
            }
            
        }
    }
}
