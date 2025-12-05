using Identidade.Domain.Arguments;
using Identidade.Domain.Arguments.Base;
using Identidade.Domain.Entities;
using Identidade.Domain.Interfaces;

namespace Identidade.Application.UseCases
{
    public class CriarClienteUseCase
    {
        private readonly IIdentidadeGateway _identidadeGateway;

        public CriarClienteUseCase(IIdentidadeGateway identidadeGateway)
        {
            _identidadeGateway = identidadeGateway;
        }

        public static CriarClienteUseCase Create(IIdentidadeGateway identidadeGateway)
        {
            return new CriarClienteUseCase(identidadeGateway);
        }

        public async Task<ClienteEntity?> ExecuteAsync(ClienteRequest request)
        {
            // Validar se cliente já existe
            var clienteExistente = await _identidadeGateway.ObterClientePorEmailAsync(request.Email);
            if (clienteExistente != null)
            {
                return null; // Cliente já existe
            }

            clienteExistente = await _identidadeGateway.ObterClientePorCpfAsync(request.Cpf);
            if (clienteExistente != null)
            {
                return null; // CPF já existe
            }

            var clienteEntity = new ClienteEntity
            {
                Nome = request.Nome,
                Email = request.Email,
                Cpf = request.Cpf,
                Status = true,
                PerfilClienteId = request.PerfilClienteId,
                DataCriacao = DateTime.Now
            };

            return await _identidadeGateway.CriarClienteAsync(clienteEntity);
        }
    }
}
