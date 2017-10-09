using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPServer
{
    public class Server
    {
        static NetworkStream firstStream = null;
        static NetworkStream secondStream = null;

        static void Main(string[] args)
        {
            Thread firstClientThread = null;
            Thread secondClientThread = null;

            FirstServer firstServer = new FirstServer();
            SecondServer secondServer = new SecondServer();

            firstClientThread = new Thread(StartFirstServer);
            firstClientThread.Start();

            secondClientThread = new Thread(StartSecondServer);
            secondClientThread.Start();

        }

        public static void StartFirstServer()
        {
            TcpListener server = null;
            TcpClient client = null;
            try
            {
                IPAddress localAddress = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddress, 5001);

                server.Start();

                Console.WriteLine("Ожидание подключений к порту 5001... ");

                client = server.AcceptTcpClient();

                Byte[] bytes = new Byte[256];
                String data = null;

                Console.WriteLine("Подключен клиент к порту 5001.");

                firstStream = client.GetStream();

                int i;

                while ((i = firstStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    secondStream.Write(bytes, 0, i);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
                client.Close();
            }
        }

        public static void StartSecondServer()
        {
            TcpListener server = null;
            TcpClient client = null;
            try
            {
                IPAddress localAddress = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddress, 5002);

                server.Start();

                Console.WriteLine("Ожидание подключений к порту 5002... ");

                client = server.AcceptTcpClient();

                Byte[] bytes = new Byte[256];
                String data = null;

                Console.WriteLine("Подключен клиент к порту 5002.");

                secondStream = client.GetStream();
                
                int i;

                while ((i = secondStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    firstStream.Write(bytes, 0, i);

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
                client.Close();
            }
        }
    }
}

