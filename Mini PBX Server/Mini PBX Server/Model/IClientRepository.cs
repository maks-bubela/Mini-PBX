using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_PBX.Models;

namespace Mini_PBX_Server.Model
{
    public interface IClientRepository
    {
        string GetClientUserName(string phone_number);
        string GetClientPhoneNumber(string userName);
        ClientDTO GetClient(string phone_number, string userName);
        void clientRegistration(string phone_number, string userName);
        bool clientLogin(string phone_number, string userName);

    }
}
