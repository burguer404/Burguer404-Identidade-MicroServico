using Identidade.Domain.Arguments;
using Identidade.Domain.Interfaces;

namespace Identidade.Application.UseCases
{
    public class AutenticarClienteUseCase
    {
        private readonly IIdentidadeGateway _identidadeGateway;

        public AutenticarClienteUseCase(IIdentidadeGateway identidadeGateway)
        {
            _identidadeGateway = identidadeGateway;
        }

        public async Task<LoginResponse> ExecutarAsync(LoginRequest request)
        {
            return await _identidadeGateway.AutenticarClienteAsync(request);
        }
    }
}
