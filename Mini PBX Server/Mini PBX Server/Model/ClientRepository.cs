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

        public string GetClientUserName(string phone_number)
        {
            ClientContext context = new ClientContext();
            var client = context.clientDTO
                       .Where(c => c.phone_number == phone_number).ToList();
            if (client.Count > 0)
                return client[0].userName;
            return "Client with this phone number not exist";
        }

        public string GetClientPhoneNumber(string userName)
        {
            ClientContext context = new ClientContext();
            var client = context.clientDTO
                       .Where(c => c.userName == userName).ToList();
            if (client.Count > 0)
                return client[0].phone_number;
            return "Client with this user name not exist";
        }

        public ClientDTO GetClient(string phone_number,string userName)
        {
            ClientContext context = new ClientContext();
            var client = context.clientDTO
                       .Where(c => c.phone_number == phone_number)
                       .Where(c => c.userName == userName).ToList();
            if (client.Count > 0)
                return client[0];
            return null;
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
