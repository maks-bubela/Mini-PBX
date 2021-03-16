using System.Data.Entity;

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
        public ClientContext(Client client)
        {
            this.client.Add(client);
            this.SaveChanges();
        }
        public DbSet<Client> client { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientConfiguration());
        }
    }


}