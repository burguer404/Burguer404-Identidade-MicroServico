using Identidade.Domain.Entities.Base;

namespace Identidade.Domain.Entities
{
    public class TokenEntity : EntityBase
    {
        public int ClienteId { get; set; }
        public virtual ClienteEntity Cliente { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
        public DateTime DataExpiracao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
