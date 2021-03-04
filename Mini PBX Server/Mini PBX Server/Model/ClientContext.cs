using Mini_PBX.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace Mini_PBX_Server.Model
{
    public class ClientContext : DbContext
    {
        public ClientContext()
            : base("name=ClientContext")
        {
        }
        public DbSet<ClientDTO> clientDTO { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientConfiguration());
        }
    }


}