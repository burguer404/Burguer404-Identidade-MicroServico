# Burguer404 - Microserviço de Identidade

Microserviço responsável pela autenticação e gestão de clientes da lanchonete **Burguer404**. Gerencia o cadastro de usuários, identificação (incluindo acesso anônimo) e emissão de tokens JWT.

## Tecnologias Utilizadas

- **.NET 8.0**: Framework principal.
- **Entity Framework Core 8.0**: ORM para acesso a dados.
- **SQL Server**: Banco de dados relacional.
- **JWT (JSON Web Token)**: Autenticação segura.
- **AutoMapper**: Mapeamento de objetos.
- **Docker**: Containerização.
- **Kubernetes (AKS)**: Orquestração.
- **Terraform**: Infraestrutura como código.

## Arquitetura

O projeto segue a **Clean Architecture**:

- **Identidade.Api**: Controllers e configurações de segurança (JWT).
- **Identidade.Application**: Casos de uso (Login, Cadastro, Identificação).
  - *Use Cases*: CriarCliente, LoginCliente, LoginClienteAnonimo, ObterClientePorId.
- **Identidade.Domain**: Entidades (Cliente, Token) e interfaces.
- **Identidade.Infrastructure**: Repositórios e contexto do banco de dados.
- **Identidade.Tests**: Testes unitários e BDD.

## Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- SQL Server

## Como Executar Localmente

1. **Clone o repositório**
   ```bash
   git clone https://github.com/burguer404/Burguer404-Identidade-MicroServico.git
   cd Burguer404-Identidade-MicroServico
   ```

2. **Configure o Banco e JWT**
   No arquivo `Identidade.Api/appsettings.Development.json`:
   ```json
   "ConnectionStrings": {
     "IdentidadeConnection": "Server=localhost;Database=IdentidadeDB;User Id=sa;Password=SuaSenhaForte;TrustServerCertificate=True;"
   },
   "Jwt": {
     "Key": "MinhaChaveSecretaSuperSegura123456789",
     "Issuer": "Burguer404",
     "Audience": "Burguer404"
   }
   ```

3. **Compile e Execute**
   ```bash
   dotnet restore
   dotnet run --project Identidade.Api
   ```

4. **Acesse a Documentação**
   Abra: `http://localhost:5000/swagger`.

## Como Executar com Docker

1. **Build da Imagem**
   ```bash
   docker build -t burguer404-identidade -f Identidade.Api/Dockerfile .
   ```

2. **Rodar o Container**
   ```bash
   docker run -d -p 5000:8080 --name identidade-api \
     -e ConnectionStrings__IdentidadeConnection="Server=host.docker.internal;Database=IdentidadeDB;User Id=sa;Password=SuaSenhaForte;TrustServerCertificate=True;" \
     burguer404-identidade
   ```

## Endpoints Principais

- `POST /api/cliente`: Cadastra um novo cliente.
- `POST /api/cliente/login`: Realiza login e retorna token JWT.
- `POST /api/cliente/identificar`: Identificação anônima ou por CPF.
- `GET /api/cliente/{id}`: Obtém dados do cliente.

## Infraestrutura e Deploy

O pipeline de CI/CD (GitHub Actions) executa testes, provisiona a infraestrutura Azure via Terraform e realiza o deploy no cluster Kubernetes (AKS).
