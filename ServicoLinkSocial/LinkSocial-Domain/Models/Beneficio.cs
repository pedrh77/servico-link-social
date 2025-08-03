namespace LinkSocial_Domain.Models
{
    public class Beneficio : EntityBase
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
