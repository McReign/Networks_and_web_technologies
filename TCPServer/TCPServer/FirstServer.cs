using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPServer
{
    class FirstServer
    {
        public static NetworkStream firstStream = null;

        public void StartServer()
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
                    SecondServer.secondStream.Write(bytes, 0, i);
                    data = Encoding.UTF8.GetString(bytes);

                    using (StreamWriter streamWriter = new StreamWriter(@"..\..\Messages.txt", true, Encoding.UTF8))
                    {
                        streamWriter.WriteLine("From 5001 to 5002 : " + data);
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
