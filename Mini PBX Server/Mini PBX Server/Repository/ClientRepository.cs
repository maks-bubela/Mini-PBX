using System.Linq;

namespace Mini_PBX_Server.Model
{
    class ClientRepository : IClientRepository
    {
        private ClientContext context;
        public ClientRepository(ClientContext context)
        {
            if(context != null )
                this.context = context;
        }
        public Client GetClient(string phone_number,string userName)
        {
            return context.client.FirstOrDefault(u => IsClientExist(phone_number, userName));
        }
        public void AddClientToDataBase(string phone_number, string userName)
        {
            var client = new Client
            {
                phone_number = phone_number,
                userName = userName
            };
            context.client.Add(client);
            context.SaveChanges();
        }
        public bool IsClientExist(string phone_number, string userName)
        {
            return context.client.Where(o => o.phone_number == phone_number).Any(o => o.userName == userName);
        }
    }
}
