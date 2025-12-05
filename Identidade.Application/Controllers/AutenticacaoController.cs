using Identidade.Domain.Arguments;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Identidade.Application.Controllers
{
    public class AutenticacaoController
    {
        private readonly IConfiguration _configuration;

        public AutenticacaoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarJwt(ClienteResponse cliente)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "chave_secreta_muito_longa_para_jwt"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString()),
                new Claim(ClaimTypes.Name, cliente.Nome),
                new Claim(ClaimTypes.Email, cliente.Email),
                new Claim("Cpf", cliente.Cpf)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "Burguer404",
                audience: _configuration["Jwt:Audience"] ?? "Burguer404",
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
