using Identidade.Domain.Entities.Base;

namespace Identidade.Domain.Entities
{
    public class PerfilClienteEntity : EntityBase
    {
        public string Descricao { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}
