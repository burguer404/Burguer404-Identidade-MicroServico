using Identidade.Domain.Entities.Base;

namespace Identidade.Domain.Entities
{
    public class ClienteEntity : EntityBase
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public int PerfilClienteId { get; set; }
        public virtual PerfilClienteEntity PerfilCliente { get; set; } = null!;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }
    }
}
