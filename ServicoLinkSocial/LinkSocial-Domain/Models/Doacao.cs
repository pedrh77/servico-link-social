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

    }


}
