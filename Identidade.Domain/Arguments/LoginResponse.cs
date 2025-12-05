using Identidade.Domain.Arguments.Base;

namespace Identidade.Domain.Arguments
{
    public class LoginResponse : ArgumentBase
    {
        public string Token { get; set; } = string.Empty;
        public DateTime DataExpiracao { get; set; }
        public ClienteResponse Cliente { get; set; } = new ClienteResponse();
    }
}
