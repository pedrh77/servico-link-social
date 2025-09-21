using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Models
{
    public class Transacao : EntityBase
    {
        public int Id { get; set; }
        public int CarteiraId { get; set; }
        public Carteira Carteira { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public StatusPagamento Status { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int? ReceiverId { get; set; }
        public Usuario? Receiver { get; set; }
        public string NomeTransacao { get; set; }
    }
}
