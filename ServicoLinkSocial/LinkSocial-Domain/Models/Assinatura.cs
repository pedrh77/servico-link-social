using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Models
{
    public class Assinatura
    {
        public int Id { get; set; }
        public int OngId { get; set; }
        public Usuario Ong { get; set; }
        public int DoadorId { get; set; }
        public Usuario Doador { get; set; }
        public TipoDoacao TipoDoacao { get; set; }
        public DateTime Inicio { get; set; } = DateTime.UtcNow;
        public DateTime Termina_em { get; set; }
        public bool Pago { get; set; }

        public int AssinaturaId { get; set; }
        public Beneficio Beneficio { get; set; }
    }
}
