using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Mini_PBX.Models
{
    public class ClientContext : DbContext
    {
        public ClientContext()
            : base ("name=ClientContext")
        {
        }
    }
}
