using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Mini_PBX
{
    public class ClientObject
    {
        private string phone_number;
        protected internal NetworkStream Stream { get; private set; }
        TcpClient client;
        ServerObject server; // объект сервера
        public string GetPhone_number() { return phone_number; }

        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
        }

        public void Process()
        {
            try
            {
                string call_numb="";
                Stream = client.GetStream();
                // Get phone number
                string message = GetMessage();
                phone_number = message;

                message = phone_number + ": На линии ";
                Console.WriteLine(message);

                message = GetMessage();
                message = phone_number + ": " + message;
                for(int i = 5; i < message.Length; i++)
                {
                    if(message[i]!>='0' && message[i] <= '9')
                         call_numb += message[i];
                }
                server.CheckAndConect(call_numb, this);
                //server.BroadcastMessage(message, this.phone_number);
                Console.WriteLine(message);
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        message = String.Format("{0}: {1}", phone_number, message);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.phone_number);
                    }
                    catch
                    {
                        message = String.Format("{0}: Сбросил", phone_number);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.phone_number);
                        server.RemoveCall(this);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // if exit from cycle
                server.RemoveConnection(this.phone_number);
                Close();
            }
        }

        private string GetMessage()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
