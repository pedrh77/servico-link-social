using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Models
{
    public class Parcela
    {
        public int Id { get; set; }
        public int NumeroParcela { get; set; }
        public DateTime Vencimento { get; set; }
        public StatusPagamento Status { get; set; }
        public decimal Valor { get; set; }
    }
}
