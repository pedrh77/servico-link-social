using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.DTO.Response
{
    public class UsuarioResponseDTO
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string? Comentario { get; set; }
        public bool Ativo { get; set; }
    }
}
