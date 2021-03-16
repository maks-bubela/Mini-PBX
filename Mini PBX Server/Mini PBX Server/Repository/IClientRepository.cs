
namespace Mini_PBX_Server.Model
{
    public interface IClientRepository
    {
        Client GetClient(string phone_number, string userName);
        void AddClientToDataBase(string phone_number, string userName);
        bool IsClientExist(string phone_number, string userName);

    }
}
