using Microsoft.EntityFrameworkCore;
using Identidade.Domain.Entities;

namespace Identidade.Infrastructure.ContextDb
{
    public class IdentidadeContext : DbContext
    {
        public IdentidadeContext(DbContextOptions<IdentidadeContext> options) : base(options)
        {
        }

        public DbSet<ClienteEntity> Clientes { get; set; }
        public DbSet<PerfilClienteEntity> PerfisCliente { get; set; }
        public DbSet<TokenEntity> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações das entidades
            modelBuilder.Entity<ClienteEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Cpf).IsRequired().HasMaxLength(11);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Cpf).IsUnique();
                entity.HasOne(e => e.PerfilCliente)
                      .WithMany()
                      .HasForeignKey(e => e.PerfilClienteId);
            });

            modelBuilder.Entity<PerfilClienteEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<TokenEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired().HasMaxLength(500);
                entity.HasOne(e => e.Cliente)
                      .WithMany()
                      .HasForeignKey(e => e.ClienteId);
            });

            // Seed data
            modelBuilder.Entity<PerfilClienteEntity>().HasData(
                new PerfilClienteEntity { Id = 1, Descricao = "Cliente Padrão", Ativo = true },
                new PerfilClienteEntity { Id = 2, Descricao = "Cliente VIP", Ativo = true }
            );
        }
    }
}
