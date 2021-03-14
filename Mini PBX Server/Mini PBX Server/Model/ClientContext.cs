using Mini_PBX.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace Mini_PBX_Server.Model
{
    public class ClientContext : DbContext
    {
        public ClientContext()
        {

        }
        public ClientContext(string connectionString) : base(connectionString)
        {
        }
        public DbSet<Client> client { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientConfiguration());
        }
    }


}