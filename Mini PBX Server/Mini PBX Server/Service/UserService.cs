using Mini_PBX.Models;
using Mini_PBX_Server.Model;

namespace Mini_PBX_Server.Service
{
    class UserService
    {
        private ClientRepository repository;
        public UserService(ClientRepository repository)
        {
            if (repository != null)
                this.repository = repository;
        }
        public void ClientRegister(ClientDTO clientDTO)
        {
            repository.AddClientToDataBase(clientDTO.userName, clientDTO.phone_number);
        }
        public void ClientLogin(ClientDTO clientDTO)
        {
            repository.GetClient(clientDTO.userName, clientDTO.phone_number);
        }
        public bool IsUserExist(ClientDTO clientDTO)
        {
            return repository.IsClientExist(clientDTO.userName, clientDTO.phone_number);
        }
    }
}
