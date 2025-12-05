using Identidade.Application.UseCases;
using Identidade.Domain.Entities;
using Identidade.Domain.Interfaces;
using Identidade.Domain.Arguments;
using Moq;
using Xunit;

namespace Identidade.Tests.UseCases
{
    public class CriarClienteUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_DeveRetornarCliente_QuandoDadosValidos()
        {
            // Arrange
            var request = new ClienteRequest 
            { 
                Nome = "Teste", 
                Email = "teste@teste.com", 
                Cpf = "12345678900" 
            };
            
            var clienteEntity = new ClienteEntity 
            { 
                Nome = request.Nome, 
                Email = request.Email, 
                Cpf = request.Cpf 
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.CriarClienteAsync(It.IsAny<ClienteEntity>()))
                      .ReturnsAsync(clienteEntity);

            var useCase = new CriarClienteUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Nome, result.Nome);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Cpf, result.Cpf);
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarNull_QuandoDadosInvalidos()
        {
            // Arrange
            var request = new ClienteRequest 
            { 
                Nome = "", 
                Email = "", 
                Cpf = "" 
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            var useCase = new CriarClienteUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarNull_QuandoEmailJaExiste()
        {
            // Arrange
            var request = new ClienteRequest 
            { 
                Nome = "Teste", 
                Email = "existente@teste.com", 
                Cpf = "12345678900" 
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ObterClientePorEmailAsync(request.Email))
                      .ReturnsAsync(new ClienteEntity { Email = request.Email });
            
            var useCase = new CriarClienteUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarNull_QuandoCpfJaExiste()
        {
            // Arrange
            var request = new ClienteRequest 
            { 
                Nome = "Teste", 
                Email = "teste@teste.com", 
                Cpf = "12345678900" 
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ObterClientePorCpfAsync(request.Cpf))
                      .ReturnsAsync(new ClienteEntity { Cpf = request.Cpf });
            
            var useCase = new CriarClienteUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.Null(result);
        }
    }

    public class ListarClientesUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_DeveRetornarListaDeClientes()
        {
            // Arrange
            var clientes = new List<ClienteEntity> 
            { 
                new ClienteEntity { Nome = "Teste 1" },
                new ClienteEntity { Nome = "Teste 2" }
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ListarClientesAsync())
                      .ReturnsAsync(clientes);
            
            var useCase = new ListarClientesUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarListaVazia_QuandoNaoHaClientes()
        {
            // Arrange
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ListarClientesAsync())
                      .ReturnsAsync(new List<ClienteEntity>());
            
            var useCase = new ListarClientesUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }

    public class ObterClientePorIdUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_DeveRetornarCliente_QuandoIdValido()
        {
            // Arrange
            var clienteId = 1;
            var cliente = new ClienteEntity 
            { 
                Id = clienteId, 
                Nome = "Teste" 
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ObterClientePorIdAsync(clienteId))
                      .ReturnsAsync(cliente);
            
            var useCase = new ObterClientePorIdUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(clienteId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(clienteId, result.Id);
            Assert.Equal("Teste", result.Nome);
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarNull_QuandoClienteNaoExiste()
        {
            // Arrange
            var clienteId = 999;
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ObterClientePorIdAsync(clienteId))
                      .ReturnsAsync((ClienteEntity)null!);
            
            var useCase = new ObterClientePorIdUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(clienteId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarNull_QuandoIdInvalido()
        {
            // Arrange
            var gatewayMock = new Mock<IIdentidadeGateway>();
            var useCase = new ObterClientePorIdUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(0);

            // Assert
            Assert.Null(result);
        }
    }

    public class LoginClienteUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_DeveRetornarCliente_QuandoCpfValido()
        {
            // Arrange
            var cpf = "12345678900";
            var cliente = new ClienteEntity 
            { 
                Cpf = cpf, 
                Nome = "Teste",
                Status = true
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ObterClientePorCpfAsync(cpf))
                      .ReturnsAsync(cliente);
            
            var useCase = new LoginClienteUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(cpf);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cpf, result.Cpf);
            Assert.Equal("Teste", result.Nome);
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarNull_QuandoCpfInvalido()
        {
            // Arrange
            var cpf = "";
            var gatewayMock = new Mock<IIdentidadeGateway>();
            var useCase = new LoginClienteUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(cpf);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarNull_QuandoClienteNaoExiste()
        {
            // Arrange
            var cpf = "12345678900";
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ObterClientePorCpfAsync(cpf))
                      .ReturnsAsync((ClienteEntity)null!);
            
            var useCase = new LoginClienteUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(cpf);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarCliente_QuandoClienteInativo()
        {
            // Arrange
            var cpf = "12345678900";
            var cliente = new ClienteEntity 
            { 
                Cpf = cpf, 
                Nome = "Teste",
                Status = false
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ObterClientePorCpfAsync(cpf))
                      .ReturnsAsync(cliente);
            
            var useCase = new LoginClienteUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(cpf);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cpf, result.Cpf);
            Assert.Equal("Teste", result.Nome);
            Assert.False(result.Status);
        }
    }

    public class LoginClienteAnonimoUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_DeveRetornarClienteAnonimo()
        {
            // Arrange
            var clienteAnonimo = new ClienteEntity 
            { 
                Nome = "Cliente Anônimo",
                Email = "anonimo@temp.com",
                Cpf = "00000000001",
                Status = true,
                PerfilClienteId = 1
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.CriarClienteAsync(It.IsAny<ClienteEntity>()))
                      .ReturnsAsync(clienteAnonimo);
            
            var useCase = new LoginClienteAnonimoUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Cliente Anônimo", result.Nome);
            Assert.Contains("anonimo", result.Email.ToLower());
            Assert.True(result.Status);
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarNull_QuandoErroAoCriar()
        {
            // Arrange
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.CriarClienteAsync(It.IsAny<ClienteEntity>()))
                      .ReturnsAsync((ClienteEntity)null!);
            
            var useCase = new LoginClienteAnonimoUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Null(result);
        }
    }

    public class AlterarStatusClienteUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_DeveRetornarSucesso_QuandoClienteExiste()
        {
            // Arrange
            var clienteId = 1;
            var cliente = new ClienteEntity 
            { 
                Id = clienteId, 
                Nome = "Teste",
                Status = true
            };
            
            var clienteAtualizado = new ClienteEntity 
            { 
                Id = clienteId, 
                Nome = "Teste",
                Status = false
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ObterClientePorIdAsync(clienteId))
                      .ReturnsAsync(cliente);
            gatewayMock.Setup(g => g.AtualizarClienteAsync(clienteId, It.IsAny<ClienteRequest>()))
                      .ReturnsAsync(clienteAtualizado);
            
            var useCase = new AlterarStatusClienteUseCase(gatewayMock.Object);

            // Act
            var (sucesso, mensagem) = await useCase.ExecuteAsync(clienteId);

            // Assert
            Assert.True(sucesso);
            Assert.Contains("sucesso", mensagem.ToLower());
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarFalha_QuandoClienteNaoExiste()
        {
            // Arrange
            var clienteId = 999;
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ObterClientePorIdAsync(clienteId))
                      .ReturnsAsync((ClienteEntity)null!);
            
            var useCase = new AlterarStatusClienteUseCase(gatewayMock.Object);

            // Act
            var (sucesso, mensagem) = await useCase.ExecuteAsync(clienteId);

            // Assert
            Assert.False(sucesso);
            Assert.Contains("não encontrado", mensagem.ToLower());
        }

        [Fact]
        public async Task ExecuteAsync_DeveRetornarFalha_QuandoErroAoAtualizar()
        {
            // Arrange
            var clienteId = 1;
            var cliente = new ClienteEntity 
            { 
                Id = clienteId, 
                Nome = "Teste",
                Status = true
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.ObterClientePorIdAsync(clienteId))
                      .ReturnsAsync(cliente);
            gatewayMock.Setup(g => g.AtualizarClienteAsync(clienteId, It.IsAny<ClienteRequest>()))
                      .ReturnsAsync((ClienteEntity)null!);
            
            var useCase = new AlterarStatusClienteUseCase(gatewayMock.Object);

            // Act
            var (sucesso, mensagem) = await useCase.ExecuteAsync(clienteId);

            // Assert
            Assert.False(sucesso);
            Assert.Contains("erro", mensagem.ToLower());
        }
    }

    public class AutenticarClienteUseCaseTests
    {
        [Fact]
        public async Task ExecutarAsync_DeveRetornarLoginResponse_QuandoCredenciaisValidas()
        {
            // Arrange
            var request = new LoginRequest 
            { 
                Email = "teste@teste.com", 
                Cpf = "12345678900" 
            };
            
            var loginResponse = new LoginResponse 
            { 
                Id = 1,
                Token = "jwt_token_123",
                DataExpiracao = DateTime.Now.AddHours(1),
                Cliente = new ClienteResponse { Nome = "Teste" }
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.AutenticarClienteAsync(request))
                      .ReturnsAsync(loginResponse);
            
            var useCase = new AutenticarClienteUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecutarAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("jwt_token_123", result.Token);
            Assert.NotNull(result.Cliente);
        }

        [Fact]
        public async Task ExecutarAsync_DeveRetornarFalha_QuandoCredenciaisInvalidas()
        {
            // Arrange
            var request = new LoginRequest 
            { 
                Email = "invalido@teste.com", 
                Cpf = "00000000000" 
            };
            
            var loginResponse = new LoginResponse 
            { 
                Id = 0,
                Token = "",
                DataExpiracao = DateTime.MinValue,
                Cliente = new ClienteResponse()
            };
            
            var gatewayMock = new Mock<IIdentidadeGateway>();
            gatewayMock.Setup(g => g.AutenticarClienteAsync(request))
                      .ReturnsAsync(loginResponse);
            
            var useCase = new AutenticarClienteUseCase(gatewayMock.Object);

            // Act
            var result = await useCase.ExecutarAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Token);
        }
    }
}