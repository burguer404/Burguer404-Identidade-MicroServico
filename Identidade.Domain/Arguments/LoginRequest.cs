using System.ComponentModel.DataAnnotations;

namespace Identidade.Domain.Arguments
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos")]
        public string Cpf { get; set; } = string.Empty;
    }
}
