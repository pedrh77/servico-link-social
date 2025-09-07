using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.DTO.Response
{
    public class DoacaoResponseDTO
    {
        public int Id { get; set; }
        public int DoadorId { get; set; }
        public string? NomeDoador { get; set; }
        public int OngId { get; set; }
        public string NomeOng { get; set; }
        public decimal Valor { get; set; }
        public TipoDoacao TipoDoacao { get; set; }
        public DateTime CriadoEm { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public bool Anonima { get; set; } = false;
        public string? Comentario { get; set; }
    }
} 