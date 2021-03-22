using Mini_PBX.Models;
using Mini_PBX_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_PBX_Server.Service
{
    class UserService
    {
        public void ClientRegister(ClientDTO clientDTO)
        {
            new ClientRepository(new ClientContext()).AddClientToDataBase(clientDTO.userName, clientDTO.phone_number);
        }
        public void ClientLogin(ClientDTO clientDTO)
        {
            new ClientRepository(new ClientContext()).GetClient(clientDTO.userName, clientDTO.phone_number);
        }
        public void IsUserExist(ClientDTO clientDTO)
        {
            new ClientRepository(new ClientContext()).IsClientExist(clientDTO.userName, clientDTO.phone_number);
        }
    }
}
