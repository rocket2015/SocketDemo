using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace SocketDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            Thread thread = new Thread(StartServer);
            thread.Start(server);
            //Console.ReadLine();
        }

        static void StartServer(object obj)
        {
            var server = obj as Server;
            server.Start();
        }
    }
}
