using System.Linq;

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
