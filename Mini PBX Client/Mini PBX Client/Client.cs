﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Mini_PBX_Client
{
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
