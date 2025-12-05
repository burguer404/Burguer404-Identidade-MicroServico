using Identidade.Domain.Arguments;
using Identidade.Domain.Entities;

namespace Identidade.Domain.Interfaces
{
    public interface IIdentidadeGateway
    {
        Task<ClienteEntity?> CriarClienteAsync(ClienteEntity cliente);
        Task<ClienteEntity?> ObterClientePorIdAsync(int id);
        Task<ClienteEntity?> ObterClientePorEmailAsync(string email);
        Task<ClienteEntity?> ObterClientePorCpfAsync(string cpf);
        Task<List<ClienteEntity>> ListarClientesAsync();
        Task<ClienteEntity?> AtualizarClienteAsync(int id, ClienteRequest request);
        Task<bool> ExcluirClienteAsync(int id);
        Task<LoginResponse> AutenticarClienteAsync(LoginRequest request);
        Task<bool> ValidarTokenAsync(string token);
        Task<bool> RevogarTokenAsync(string token);
    }
}
