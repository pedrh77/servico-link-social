using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.DTO.Response
{
    public class TransacaoResponseDTO
    {
        public int Id { get; set; }
        public string NomeTransacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; } 
        public StatusPagamento Status { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int? ReceiverId { get; set; }
        
    }
}
