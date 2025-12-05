using System.ComponentModel.DataAnnotations;

namespace Identidade.Domain.Arguments
{
    public class ClienteRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos")]
        public string Cpf { get; set; } = string.Empty;

        public int PerfilClienteId { get; set; } = 1;
        
        public bool Status { get; set; } = true;
    }
}
