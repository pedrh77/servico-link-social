using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.DTO.Request
{
    public class NovaTransacaoRequestDTO
    {
        public int DoadorId { get; set; }
        public int? EmpresaId { get; set; }
        public TipoTransacao Tipo { get; set; } 
        public decimal Valor { get; set; } //Descontado da Carteira
        public string? Comentario { get; set; } = null;
        public decimal ValorTotal { get; set; } // Valor do consumido na empresa = 2* Valor

    }
}
