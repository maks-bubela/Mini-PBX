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
            ClientContext context = new ClientContext();
            Client client = new Client();
            var clientDTO = context.clientDTO
                       .Where(c => c.phone_number == phone_number)
                       .Where(c => c.userName == userName).ToList();
            if (clientDTO.Count > 0)
            {
                client.Id = clientDTO[0].Id;
                client.phone_number = clientDTO[0].phone_number;
                client.userName = clientDTO[0].userName;
                return client;
            }
            return null;
        }


        public void AddClientToDataBase(string phone_number, string userName)
        {
            ClientContext context = new ClientContext();
            ClientDTO newClient = new ClientDTO();
            newClient.phone_number = phone_number;
            newClient.userName = userName;
            context.clientDTO.Add(newClient);
            context.SaveChanges();
        }
        public bool IsClientExist(string phone_number, string userName)
        {
            ClientContext context = new ClientContext();
            var client = context.clientDTO
                       .Where(c => c.phone_number == phone_number)
                       .Where(c => c.userName == userName).ToList();

            if (client.Count > 0 && client[0].phone_number == phone_number && client[0].userName == userName)
                return true;
            else
                return false;
        }
    }
}
