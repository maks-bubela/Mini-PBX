using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Mini_PBX_Server.Model;
using Mini_PBX.Models;

namespace Mini_PBX
{
    public class ClientObject
    {
        private string phone_number;

        protected internal NetworkStream Stream { get; private set; }
        TcpClient client;
        ServerObject server; // объект сервера
        Action<string, string> _callback;
        public string GetPhone_number() { return phone_number; }
        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
        }

        public void Process()
        {
            string message = "";
            string call_numb = "";
            string userName = "";
            try
            {
                Stream = client.GetStream();
                // Get phone number
                message = GetMessage();
                for (int i = 0; i < message.Length; i++)
                {
                    if (message[i] >= '0' && message[i] <= '9')
                        phone_number += message[i];
                    else if ((message[i] >= 'a' && message[i] <= 'z') || (message[i] >= 'A' && message[i] <= 'Z'))
                        userName += message[i];
                }
                if (phone_number.Length == 3)
                {
                    if (!clientLogin(phone_number, userName)) { }
                        clientRegistration(phone_number, userName);
                    message = phone_number + ": На линии ";
                    Console.WriteLine(message);

                    message = GetMessage();
                    message = phone_number + ": " + message;
                    for(int i = 5; i < message.Length; i++)
                    {
                        if(message[i] >='0' && message[i] <= '9')
                             call_numb += message[i];
                    }
                    server.CheckAndConect(call_numb, this);
                    Console.WriteLine(message);
                    bool check = true;
                    while (check)
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
                            check = false;
                        }
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

        public void clientRegistration(string phone_number, string userName)
        {
            ClientContext context = new ClientContext();
            ClientDTO newClient = new ClientDTO();
            newClient.phone_number = phone_number;
            newClient.userName = userName;
            context.clientDTO.Add(newClient);
            context.SaveChanges();
        }
        public bool clientLogin(string phone_number, string userName)
        {
            ClientDTO clientDTO = new ClientDTO();
            if (clientDTO.phone_number == phone_number && clientDTO.userName == userName)
                return true;
            else
                return false;
        }

        public Task ProcessAsync()
        {
            return Task.Run(() =>
            {
                Process();
            });
        }
        public string GetMessage()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
             bytes = Stream.Read(data, 0, data.Length);
            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            return builder.ToString();
        }

        public void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
