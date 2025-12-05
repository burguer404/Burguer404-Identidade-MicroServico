using Identidade.Domain.Arguments;
using Identidade.Domain.Arguments.Base;
using Identidade.Domain.Interfaces;
using Identidade.Application.Presenters;
using Identidade.Application.UseCases;
using Identidade.Infrastructure.Auth.Autenticacao;
using Microsoft.Extensions.Configuration;

namespace Identidade.Application.Controllers
{
    public class ClienteController
    {
        private IIdentidadeGateway _gateway;
        private IConfiguration _configuration;

        public ClienteController(IIdentidadeGateway gateway, IConfiguration configuration)
        {
            _gateway = gateway;
            _configuration = configuration;
        }

        public async Task<ResponseBase<ClienteResponse>> LoginCliente(string cpf)
        {
            // Autenticação com Azure AD primeiro
            var instanciaAd = AutenticacaoAzureAd.Create(_configuration);
            var responseAd = await instanciaAd.AutenticarComAzureAd(cpf);

            if (responseAd != null && !responseAd.success)
            {
                return new ResponseBase<ClienteResponse>() { Sucesso = false, Mensagem = "Erro de autenticação com o Azure AD", Resultado = [] };
            }

            var useCase = LoginClienteUseCase.Create(_gateway);

            var cliente = await useCase.ExecuteAsync(cpf);

            if (cliente == null)
            {
                return new ResponseBase<ClienteResponse>() { Sucesso = false, Mensagem = "Cliente não encontrado!", Resultado = [] };
            }

            return IdentidadePresenter.ObterClienteResponse(cliente);
        }

        public async Task<ResponseBase<ClienteResponse>> CadastrarCliente(ClienteRequest request)
        {
            var useCase = CriarClienteUseCase.Create(_gateway);

            var cliente = await useCase.ExecuteAsync(request);

            if (cliente == null)
            {
                return new ResponseBase<ClienteResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro durante o cadastro do cliente!", Resultado = [] };
            }

            return IdentidadePresenter.ObterClienteResponse(cliente);
        }

        public async Task<ResponseBase<ClienteResponse>> ListarClientes()
        {
            var useCase = ListarClientesUseCase.Create(_gateway);

            var clientes = await useCase.ExecuteAsync();

            return IdentidadePresenter.ObterListaClienteResponse(clientes);
        }

        public async Task<ResponseBase<bool>> AlterarStatusCliente(int clienteId)
        {
            var useCase = AlterarStatusClienteUseCase.Create(_gateway);

            var (sucesso, mensagem) = await useCase.ExecuteAsync(clienteId);

            return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = mensagem, Resultado = [sucesso] };
        }

        public async Task<ResponseBase<ClienteResponse>> LoginClienteAnonimo()
        {
            var useCase = LoginClienteAnonimoUseCase.Create(_gateway);

            var cliente = await useCase.ExecuteAsync();

            if (cliente == null)
            {
                return new ResponseBase<ClienteResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro durante o cadastro do cliente!", Resultado = [] };
            }

            return IdentidadePresenter.ObterClienteResponse(cliente);
        }
    }
}