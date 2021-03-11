using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_PBX.Models;

namespace Mini_PBX_Server.Model
{
    public class ClientConfiguration : EntityTypeConfiguration<ClientDTO>
    {
        public ClientConfiguration()
        {
            ToTable("ClientDTOes")
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
