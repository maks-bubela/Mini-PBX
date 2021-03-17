using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mini_PBX
{
    public class ServerObject
    {
        bool exit_checker = true;
        TcpListener tcpListener;
        List<ClientObject> clients = new List<ClientObject>();
        List<CallClients> call_clients = new List<CallClients>();
        Action<string, string> _callback;
 
        public void CloseApp(bool close)
        {
            exit_checker = close;
        }

        public void RemoveCall(ClientObject client)
        {
            for(int i = 0; i < call_clients.Count; i++)
            {
                if (call_clients[i].checkClient(client))
                    call_clients.Remove(call_clients[i]);
            }
        }

        public void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
        }
        public void RemoveConnection(string phone_number)
        {
            for(int i=0;i<clients.Count;i++)
            {
                if (clients[i].GetPhone_number() == phone_number)
                    clients.Remove(clients[i]);
            }
        }
        public void Listen()
        {
                try
                {
                    tcpListener = new TcpListener(IPAddress.Any, 8888);
                    tcpListener.Start();
                    Console.WriteLine("АТС запущен. Ожидание подключений...");
                    while (exit_checker)
                    {
                        TcpClient tcpClient =  tcpListener.AcceptTcpClient();

                        ClientObject clientObject =  new ClientObject(tcpClient, this);
                        clientObject.ProcessAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Disconnect();
                }
        }



        public void BroadcastMessage(string message, string phone_number)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < call_clients.Count; i++)
            {
                if (call_clients[i].checkClient(phone_number)) 
                {
                    if(call_clients[i].GetFirstClient().GetPhone_number() == phone_number)
                         call_clients[i].GetSecondClient().Stream.Write(data, 0, data.Length);
                    else
                        call_clients[i].GetFirstClient().Stream.Write(data, 0, data.Length);
                }
            }
        }
        public Task BroadcastMessageAsync(string message, string phone_number)
        {
            return Task.Run(() =>
            {
                BroadcastMessage(message, phone_number);
            });
        }

        public void CheckAndConect(string phone_number, ClientObject caller_client)
        {
            string message;
            byte[] data;
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].GetPhone_number() == phone_number)
                {
                    for(int j = 0; j < call_clients.Count; j++)
                    {
                        if (call_clients[j].checkClient(clients[i]))
                        {
                            message = "Клиент занят";
                            data = Encoding.Unicode.GetBytes(message);
                            caller_client.Stream.Write(data, 0, data.Length);
                            return;
                        }
                    }
                    message = "Идет подключение";
                    data = Encoding.Unicode.GetBytes(message);
                    caller_client.Stream.Write(data, 0, data.Length);
                    call_clients.Add(new CallClients(caller_client,clients[i]));
                    return;
                }
            }
            message = "Абонента с данным номером не существует";
            data = Encoding.Unicode.GetBytes(message);
            caller_client.Stream.Write(data, 0, data.Length);
        }

        public Task CheckAndConectAsync(string phone_number, ClientObject caller_client)
        {
            return Task.Run(() =>
            {
                CheckAndConect(phone_number, caller_client);
            });
        }

        public void Disconnect()
        {
            tcpListener.Stop();

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close();
            }
            Environment.Exit(0); 
        }
    }
}
