using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;

namespace Mini_PBX_Client
{
    public class Program
    {      
        static string phone_number;
        static string userName = "";
        private const string host = "127.0.0.1";
        private const int port = 8888;
        public static TcpClient client;
        public static NetworkStream stream;
       
        public static void Main(string[] args)
        {
            Client c = new Client();
            string message = userName + phone_number;
            
            Console.Write("Введите свой номер: ");
            phone_number = Console.ReadLine();

            Console.Write("Введите свое имя: ");
            userName = Console.ReadLine();
            client = new TcpClient();
            try
            {
                client.Connect(host, port);
                stream = client.GetStream();

               
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);


                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
                Console.WriteLine($"Вашие данные: {userName}:{phone_number}");
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
}



