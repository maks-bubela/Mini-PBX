using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_PBX
{
    public class CallClients
    {
        ClientObject client_1;
        ClientObject client_2;
        public CallClients(ClientObject client_1, ClientObject client_2)
        {
            this.client_1 = client_1;
            this.client_2 = client_2;
        }
        public ClientObject GetFirstClient() { return this.client_1; }
        public ClientObject GetSecondClient() { return this.client_2; }
        public bool checkClient(ClientObject client)
        {
            if (this.client_1 == client || this.client_2 == client)
                return true;
            return false;
        }
        public bool checkClient(string phone_number)
        {
            if (this.client_1.GetPhone_number() == phone_number || this.client_2.GetPhone_number() == phone_number)
                return true;
            return false;
        }

    }
}
