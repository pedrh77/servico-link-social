namespace LinkSocial_Domain.DTO.Request
{
    public class LoginRequestDTO
    {
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public string Senha { get; set; }
    }
}
