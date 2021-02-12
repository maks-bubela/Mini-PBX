using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Mini_PBX
{
    public class ServerObject
    {
        static TcpListener tcpListener;
        List<ClientObject> clients = new List<ClientObject>();
        List<CallClients> call_clients = new List<CallClients>();

        protected internal void RemoveCall(ClientObject client)
        {
            for(int i = 0; i < call_clients.Count; i++)
            {
                if (call_clients[i].checkClient(client))
                    call_clients.Remove(call_clients[i]);
            }
        }

        protected internal void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
        }
        protected internal void RemoveConnection(string phone_number)
        {
            for(int i=0;i<clients.Count;i++)
            {
                if (clients[i].GetPhone_number() == phone_number)
                    clients.Remove(clients[i]);
            }
        }
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("АТС запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        protected internal void BroadcastMessage(string message, string phone_number)
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

        protected internal void CheckAndConect(string phone_number, ClientObject caller_client)
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

        protected internal void Disconnect()
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
