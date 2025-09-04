using LinkSocial_Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace LinkSocial_Domain.DTO.Request
{
    public class NovoUsuarioRequestDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O nome deve ter no máximo 255 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }


        public string? Cpf { get; set; } = null;
        public string? Cnpj { get; set; } = null;

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        [EnumDataType(typeof(TipoUsuario))]
        public TipoUsuario TipoUsuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmaSenha { get; set; }


    }
}
