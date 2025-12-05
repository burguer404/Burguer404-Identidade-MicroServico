using Identidade.Domain.Arguments;
using Identidade.Domain.Entities;
using Identidade.Domain.Interfaces;

namespace Identidade.Application.UseCases
{
    public class LoginClienteUseCase
    {
        private readonly IIdentidadeGateway _gateway;

        public LoginClienteUseCase(IIdentidadeGateway gateway)
        {
            _gateway = gateway;
        }

        public static LoginClienteUseCase Create(IIdentidadeGateway gateway)
        {
            return new LoginClienteUseCase(gateway);
        }

        public async Task<ClienteEntity?> ExecuteAsync(string cpf)
        {
            return await _gateway.ObterClientePorCpfAsync(cpf);
        }
    }
}
