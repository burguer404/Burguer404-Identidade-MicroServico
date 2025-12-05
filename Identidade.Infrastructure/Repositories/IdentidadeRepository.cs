using Microsoft.EntityFrameworkCore;
using Identidade.Domain.Entities;
using Identidade.Infrastructure.ContextDb;

namespace Identidade.Infrastructure.Repositories
{
    public class IdentidadeRepository
    {
        private readonly IdentidadeContext _context;

        public IdentidadeRepository(IdentidadeContext context)
        {
            _context = context;
        }

        public async Task<ClienteEntity?> ObterClientePorIdAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.PerfilCliente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ClienteEntity?> ObterClientePorEmailAsync(string email)
        {
            return await _context.Clientes
                .Include(c => c.PerfilCliente)
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<ClienteEntity?> ObterClientePorCpfAsync(string cpf)
        {
            return await _context.Clientes
                .Include(c => c.PerfilCliente)
                .FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<List<ClienteEntity>> ListarClientesAsync()
        {
            return await _context.Clientes
                .Include(c => c.PerfilCliente)
                .Where(c => c.Status)
                .ToListAsync();
        }

        public async Task<ClienteEntity> CriarClienteAsync(ClienteEntity cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<ClienteEntity> AtualizarClienteAsync(ClienteEntity cliente)
        {
            cliente.DataAtualizacao = DateTime.Now;
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<bool> ExcluirClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            cliente.Status = false;
            cliente.DataAtualizacao = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TokenEntity> CriarTokenAsync(TokenEntity token)
        {
            _context.Tokens.Add(token);
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<TokenEntity?> ObterTokenAtivoAsync(string token)
        {
            return await _context.Tokens
                .Include(t => t.Cliente)
                .ThenInclude(c => c.PerfilCliente)
                .FirstOrDefaultAsync(t => t.Token == token && t.Ativo && t.DataExpiracao > DateTime.Now);
        }

        public async Task<bool> RevogarTokenAsync(string token)
        {
            var tokenEntity = await _context.Tokens.FirstOrDefaultAsync(t => t.Token == token);
            if (tokenEntity == null) return false;

            tokenEntity.Ativo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
