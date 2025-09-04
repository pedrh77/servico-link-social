using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Models
{
    public class Usuario : EntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string? SenhaHash { get; set; }
        public bool Ativo { get; set; } = true;
        public string? Comentario { get; set; } = null;

    }
}
