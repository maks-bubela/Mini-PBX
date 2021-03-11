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
        Client GetClient(string phone_number, string userName);
        void AddClientToDataBase(string phone_number, string userName);
        bool IsClientExist(string phone_number, string userName);

    }
}
