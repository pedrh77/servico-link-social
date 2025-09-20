using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.DTO.Request
{
    public class PedidoValidacaoRequestDTO
    {
        public int ClienteId { get; set; }
        public string CodigoValidacao { get; set; }
        public StatusPagamento NovoStatus { get; set; } = StatusPagamento.Aprovado;
    }
}
