using System.Data.Entity.ModelConfiguration;
using Mini_PBX.Models;

namespace Mini_PBX_Server.Model
{
    public class ClientConfiguration : EntityTypeConfiguration<ClientDTO>
    {
        public ClientConfiguration()
        {
            ToTable("Client")
                 .HasKey(u => u.Id)
                 .Property(u => u.Id)
                 .IsRequired();
            Property(u => u.phone_number)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(3);
            Property(u => u.userName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(256);
        }
    }
}
