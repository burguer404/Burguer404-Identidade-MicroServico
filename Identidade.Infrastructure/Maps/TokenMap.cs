using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Identidade.Domain.Entities;

namespace Identidade.Infrastructure.Maps
{
    public class TokenMap : IEntityTypeConfiguration<TokenEntity>
    {
        public void Configure(EntityTypeBuilder<TokenEntity> builder)
        {
            builder.ToTable("Tokens");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Token).IsRequired().HasMaxLength(500);
            builder.Property(e => e.DataExpiracao).IsRequired();
            builder.Property(e => e.Ativo).IsRequired().HasDefaultValue(true);
            builder.Property(e => e.DataCriacao).IsRequired();
            builder.HasOne(e => e.Cliente)
                   .WithMany()
                   .HasForeignKey(e => e.ClienteId);
        }
    }
}
