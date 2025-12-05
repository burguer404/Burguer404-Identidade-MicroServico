using Identidade.Domain.Arguments;
using Identidade.Domain.Entities;
using Identidade.Domain.Interfaces;

namespace Identidade.Application.UseCases
{
    public class AlterarStatusClienteUseCase
    {
        private readonly IIdentidadeGateway _gateway;

        public AlterarStatusClienteUseCase(IIdentidadeGateway gateway)
        {
            _gateway = gateway;
        }

        public static AlterarStatusClienteUseCase Create(IIdentidadeGateway gateway)
        {
            return new AlterarStatusClienteUseCase(gateway);
        }

        public async Task<(bool sucesso, string mensagem)> ExecuteAsync(int clienteId)
        {
            var cliente = await _gateway.ObterClientePorIdAsync(clienteId);
            
            if (cliente == null)
            {
                return (false, "Cliente não encontrado");
            }

            cliente.Status = !cliente.Status;
            var clienteAtualizado = await _gateway.AtualizarClienteAsync(clienteId, new ClienteRequest
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cpf = cliente.Cpf,
                Status = cliente.Status,
                PerfilClienteId = cliente.PerfilClienteId
            });

            if (clienteAtualizado == null)
            {
                return (false, "Erro ao alterar status do cliente");
            }

            return (true, "Status do cliente alterado com sucesso");
        }
    }
}
