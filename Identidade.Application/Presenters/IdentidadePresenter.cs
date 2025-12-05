using Identidade.Domain.Arguments;
using Identidade.Domain.Arguments.Base;
using Identidade.Domain.Entities;

namespace Identidade.Application.Presenters
{
    public static class IdentidadePresenter
    {
        public static ResponseBase<ClienteResponse> ObterClienteResponse(ClienteEntity cliente)
        {
            var clienteResponse = new ClienteResponse
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cpf = cliente.Cpf,
                Status = cliente.Status,
                PerfilClienteId = cliente.PerfilClienteId,
                PerfilClienteDescricao = cliente.PerfilCliente?.Descricao ?? string.Empty,
                DataCriacao = cliente.DataCriacao,
                DataAtualizacao = cliente.DataAtualizacao
            };

            return new ResponseBase<ClienteResponse>
            {
                Sucesso = true,
                Mensagem = "Cliente obtido com sucesso",
                Resultado = [clienteResponse]
            };
        }

        public static ResponseBase<ClienteResponse> ObterListaClienteResponse(List<ClienteEntity> clientes)
        {
            var clientesResponse = clientes.Select(cliente => new ClienteResponse
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cpf = cliente.Cpf,
                Status = cliente.Status,
                PerfilClienteId = cliente.PerfilClienteId,
                PerfilClienteDescricao = cliente.PerfilCliente?.Descricao ?? string.Empty,
                DataCriacao = cliente.DataCriacao,
                DataAtualizacao = cliente.DataAtualizacao
            }).ToList();

            return new ResponseBase<ClienteResponse>
            {
                Sucesso = true,
                Mensagem = "Clientes listados com sucesso",
                Resultado = clientesResponse
            };
        }
    }
}
