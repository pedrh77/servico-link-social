using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Enum
{
    public class Transacao :EntityBase
    {
        public int Id { get; set; }
        public int CarteiraId { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;

        public TipoTransacao Tipo { get; set; }
        public int? ReceiverId { get; set; }
        public Usuario? Receiver { get; set; }
    }
}
