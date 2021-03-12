using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_PBX.Models;

namespace Mini_PBX_Server.Model
{
    class ClientRepository : IClientRepository
    {
        public Client GetClient(string phone_number,string userName)
        {
            Client client = new Client();
            var clients = new ClientContext().client
                       .Where(c => c.phone_number == phone_number)
                       .Where(c => c.userName == userName).ToList();
            if (clients.Count > 0)
            {
                client.Id = clients[0].Id;
                client.phone_number = clients[0].phone_number;
                client.userName = clients[0].userName;
                return client;
            }
            return null;
        }


        public void AddClientToDataBase(string phone_number, string userName)
        {
            ClientContext context = new ClientContext();
            Client newClient = new Client();
            newClient.phone_number = phone_number;
            newClient.userName = userName;
            context.client.Add(newClient);
            context.SaveChanges();
        }
        public bool IsClientExist(string phone_number, string userName)
        {
            if (new ClientContext().client.Where(o => o.phone_number == phone_number).Any(o => o.userName == userName))
                return true;
            return false;
        }
    }
}
