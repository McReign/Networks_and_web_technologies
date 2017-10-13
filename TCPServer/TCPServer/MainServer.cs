using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPServer
{
    class MainServer
    {
        static void Main(string[] args)
        {
            FirstServer firstServer = new FirstServer();
            SecondServer secondServer = new SecondServer();

            Thread firstClientThread = new Thread(firstServer.StartServer);
            Thread secondClientThread = new Thread(secondServer.StartServer);
            
            firstClientThread.Start();
            secondClientThread.Start();

        }
    }
}

