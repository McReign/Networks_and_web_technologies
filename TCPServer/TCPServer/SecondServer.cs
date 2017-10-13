using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPServer
{
    class SecondServer
    {
        public static NetworkStream secondStream = null;

        public void StartServer()
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
                    FirstServer.firstStream.Write(bytes, 0, i);
                    data = Encoding.UTF8.GetString(bytes);

                    using (StreamWriter streamWriter = new StreamWriter(@"..\..\Messages.txt", true, Encoding.UTF8))
                    {
                        streamWriter.WriteLine("From 5002 to 5001 : " + data);
                    }
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
