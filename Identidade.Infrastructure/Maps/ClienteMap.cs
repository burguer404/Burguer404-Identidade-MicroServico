using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Identidade.Domain.Entities;

namespace Identidade.Infrastructure.Maps
{
    public class ClienteMap : IEntityTypeConfiguration<ClienteEntity>
    {
        public void Configure(EntityTypeBuilder<ClienteEntity> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Cpf).IsRequired().HasMaxLength(11);
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.Cpf).IsUnique();
            builder.Property(e => e.DataCriacao).IsRequired();
            builder.Property(e => e.Status).IsRequired().HasDefaultValue(true);
        }
    }
}
