using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Identidade.Domain.Entities;

namespace Identidade.Infrastructure.Maps
{
    public class PerfilClienteMap : IEntityTypeConfiguration<PerfilClienteEntity>
    {
        public void Configure(EntityTypeBuilder<PerfilClienteEntity> builder)
        {
            builder.ToTable("PerfisCliente");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Descricao).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Ativo).IsRequired().HasDefaultValue(true);
        }
    }
}
