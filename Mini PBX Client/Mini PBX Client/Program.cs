using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;

namespace Mini_PBX_Client
{
    public class Program
    {
        
        static string phone_number;
        private const string host = "127.0.0.1";
        private const int port = 8888;
        public static TcpClient client;
        public static NetworkStream stream;
       
        public static void Main(string[] args)
        {
            Client c = new Client();
            Console.Write("Введите свой номер: ");
            phone_number = Console.ReadLine();
            client = new TcpClient();
            try
            {
                client.Connect(host, port);
                stream = client.GetStream();

                string message = phone_number;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);


                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
                Console.WriteLine("Ваш номер: {0}", phone_number);
                c.SendMessage(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                c.Disconnect(stream, client);
            }

        }
        private static void ReceiveMessage()
        {
            Client c = new Client();
            c.ReceiveMessage(stream, client);
        }
    }
    public class Client
    {
        public void SendMessage(NetworkStream stream)
        {
            Console.Write("Введите номер: ");
            string message = Console.ReadLine();
            message = $"Вызов {message} ...";
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
            while (true)
            {
                message = Console.ReadLine();
                data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }

        public void ReceiveMessage(NetworkStream stream, TcpClient client)
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    Console.WriteLine(message);
                }
                catch
                {
                    Console.WriteLine("Конец!");
                    Console.ReadLine();
                    Disconnect(stream, client);
                }
            }
        }

        public void Disconnect(NetworkStream stream, TcpClient client)
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
            Environment.Exit(0);
        }        
    }
}



