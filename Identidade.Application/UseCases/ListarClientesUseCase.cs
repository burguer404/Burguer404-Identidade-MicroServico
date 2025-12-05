using Identidade.Domain.Entities;
using Identidade.Domain.Interfaces;

namespace Identidade.Application.UseCases
{
    public class ListarClientesUseCase
    {
        private readonly IIdentidadeGateway _identidadeGateway;

        public ListarClientesUseCase(IIdentidadeGateway identidadeGateway)
        {
            _identidadeGateway = identidadeGateway;
        }

        public static ListarClientesUseCase Create(IIdentidadeGateway identidadeGateway)
        {
            return new ListarClientesUseCase(identidadeGateway);
        }

        public async Task<List<ClienteEntity>> ExecuteAsync()
        {
            return await _identidadeGateway.ListarClientesAsync();
        }
    }
}
