using System;
using System.Net;
using System.Net.Sockets;


namespace TCPServer
{
    public class FirstServer
    {
        public static void StartServer() { 
            TcpListener server = null;
            TcpClient client = null;
            try
            {
                IPAddress localAddress = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddress, 5001);

                server.Start();

                Console.WriteLine("Ожидание подключений 5001... ");

                client = server.AcceptTcpClient();

                Byte[] bytes = new Byte[256];
                String data = null;

                Console.WriteLine("Подключен клиент.");

                NetworkStream stream = client.GetStream();

                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    stream.Write(msg, 0, msg.Length);

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
