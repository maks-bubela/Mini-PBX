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
            return new ClientContext().client.FirstOrDefault(u => IsClientExist(phone_number, userName));
        }
        public void AddClientToDataBase(string phone_number, string userName)
        {
            new ClientContext(new Client(phone_number, userName));
        }
        public bool IsClientExist(string phone_number, string userName)
        {
            return new ClientContext().client.Where(o => o.phone_number == phone_number).Any(o => o.userName == userName);
        }
    }
}
