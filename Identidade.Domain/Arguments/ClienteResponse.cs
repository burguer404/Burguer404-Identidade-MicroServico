using Identidade.Domain.Arguments.Base;

namespace Identidade.Domain.Arguments
{
    public class ClienteResponse : ArgumentBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int PerfilClienteId { get; set; }
        public string PerfilClienteDescricao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
