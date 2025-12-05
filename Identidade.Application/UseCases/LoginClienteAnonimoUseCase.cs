using Identidade.Domain.Arguments;
using Identidade.Domain.Entities;
using Identidade.Domain.Interfaces;

namespace Identidade.Application.UseCases
{
    public class LoginClienteAnonimoUseCase
    {
        private readonly IIdentidadeGateway _gateway;

        public LoginClienteAnonimoUseCase(IIdentidadeGateway gateway)
        {
            _gateway = gateway;
        }

        public static LoginClienteAnonimoUseCase Create(IIdentidadeGateway gateway)
        {
            return new LoginClienteAnonimoUseCase(gateway);
        }

        public async Task<ClienteEntity?> ExecuteAsync()
        {
            // Criar cliente anônimo
            var clienteAnonimo = new ClienteEntity
            {
                Nome = "Cliente Anônimo",
                Email = $"anonimo_{DateTime.Now.Ticks}@temp.com",
                Cpf = $"000000000{DateTime.Now.Ticks % 100}",
                Status = true,
                PerfilClienteId = 1, // Perfil padrão
                DataCriacao = DateTime.Now
            };

            return await _gateway.CriarClienteAsync(clienteAnonimo);
        }
    }
}
