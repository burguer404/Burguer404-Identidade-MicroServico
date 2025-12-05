using Identidade.Domain.Entities;
using Identidade.Domain.Interfaces;

namespace Identidade.Application.UseCases
{
    public class ObterClientePorIdUseCase
    {
        private readonly IIdentidadeGateway _identidadeGateway;

        public ObterClientePorIdUseCase(IIdentidadeGateway identidadeGateway)
        {
            _identidadeGateway = identidadeGateway;
        }

        public async Task<ClienteEntity?> ExecuteAsync(int id)
        {
            return await _identidadeGateway.ObterClientePorIdAsync(id);
        }
    }
}
