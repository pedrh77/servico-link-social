using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Models
{
    public class Doacao : EntityBase
    {
        public int Id { get; set; }

        public int DoadorId { get; set; }
        public Usuario Doador { get; set; }

        public int OngId { get; set; }
        public Usuario Ong { get; set; }

        public decimal Valor { get; set; }

        public TipoDoacao TipoDoacao { get; set; }

        public int NumeroParcela { get; set; } = 1;
        public int TotalParcelas { get; set; } = 1;

        public int? DoacaoPrincipalId { get; set; }
        public Doacao? DoacaoPrincipal { get; set; }

        public ICollection<Doacao> Parcelas { get; set; } = new List<Doacao>();

        public StatusPagamento StatusPagamento { get; set; } = StatusPagamento.Pendente;
        public bool Anonima { get; set; } = false;
        public string? Comentario { get; set; }
    }


}
