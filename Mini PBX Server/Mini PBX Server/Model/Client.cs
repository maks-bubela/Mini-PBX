using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_PBX_Server.Model
{
    public class Client
    {
        public long Id { get; set; }
        public string userName { get; set; }
        public string phone_number { get; set; }
        public Client(string phone_number, string userName)
        {
            this.userName = userName;
            this.phone_number = phone_number;
        }
    }
}
