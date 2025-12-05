using Identidade.Domain.Arguments;
using Identidade.Domain.Entities;
using Identidade.Domain.Interfaces;
using Identidade.Infrastructure.Repositories;

namespace Identidade.Application.Gateways
{
    public class IdentidadeGateway : IIdentidadeGateway
    {
        private readonly IdentidadeRepository _repository;

        public IdentidadeGateway(IdentidadeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClienteEntity?> CriarClienteAsync(ClienteEntity cliente)
        {
            try
            {
                return await _repository.CriarClienteAsync(cliente);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ClienteEntity?> ObterClientePorIdAsync(int id)
        {
            try
            {
                return await _repository.ObterClientePorIdAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ClienteEntity?> ObterClientePorEmailAsync(string email)
        {
            try
            {
                return await _repository.ObterClientePorEmailAsync(email);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ClienteEntity?> ObterClientePorCpfAsync(string cpf)
        {
            try
            {
                return await _repository.ObterClientePorCpfAsync(cpf);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<ClienteEntity>> ListarClientesAsync()
        {
            try
            {
                return await _repository.ListarClientesAsync();
            }
            catch (Exception)
            {
                return new List<ClienteEntity>();
            }
        }

        public async Task<ClienteEntity?> AtualizarClienteAsync(int id, ClienteRequest request)
        {
            try
            {
                var cliente = await _repository.ObterClientePorIdAsync(id);
                if (cliente == null)
                    return null;

                cliente.Nome = request.Nome;
                cliente.Email = request.Email;
                cliente.Cpf = request.Cpf;
                cliente.Status = request.Status;
                cliente.PerfilClienteId = request.PerfilClienteId;
                cliente.DataAtualizacao = DateTime.Now;

                return await _repository.AtualizarClienteAsync(cliente);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> ExcluirClienteAsync(int id)
        {
            try
            {
                return await _repository.ExcluirClienteAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<LoginResponse> AutenticarClienteAsync(LoginRequest request)
        {
            try
            {
                var cliente = await _repository.ObterClientePorCpfAsync(request.Cpf);
                if (cliente == null)
                {
                    return new LoginResponse
                    {
                        Token = string.Empty,
                        DataExpiracao = DateTime.MinValue,
                        Cliente = new ClienteResponse()
                    };
                }

                // Simular geração de token
                var token = $"token_{cliente.Id}_{DateTime.Now.Ticks}";
                
                return new LoginResponse
                {
                    Token = token,
                    DataExpiracao = DateTime.Now.AddHours(24),
                    Cliente = new ClienteResponse
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
                    }
                };
            }
            catch (Exception)
            {
                return new LoginResponse
                {
                    Token = string.Empty,
                    DataExpiracao = DateTime.MinValue,
                    Cliente = new ClienteResponse()
                };
            }
        }

        public async Task<bool> ValidarTokenAsync(string token)
        {
            try
            {
                // Implementação simples de validação
                return !string.IsNullOrEmpty(token) && token.StartsWith("token_");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RevogarTokenAsync(string token)
        {
            try
            {
                // Implementação simples de revogação
                return !string.IsNullOrEmpty(token);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}