using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Models
{
    public class Doacao : EntityBase
    {
        public int Id { get; set; }

        // Relacionamentos
        public int DoadorId { get; set; }
        public Usuario Doador { get; set; }

        public int OngId { get; set; }
        public Usuario Ong { get; set; }

        public int BeneficioId { get; set; }
        public Beneficio Beneficio { get; set; }

      
        public decimal ValorParcela { get; set; }
        public TipoDoacao TipoDoacao { get; set; }

        public StatusPagamento StatusPagamento { get; set; } = StatusPagamento.Pendente;
        public string? Comentario { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public int ParcelasTotais => TipoDoacao switch
        {
            TipoDoacao.Unica => 1,
            TipoDoacao.Mensal6x => 6,
            TipoDoacao.Mensal12x => 12
        };

        public DateTime DataFimPrevista => CriadoEm.AddMonths(ParcelasTotais);

        public List<Parcela> Parcelas { get; set; } = new List<Parcela>();
    }

   
}
