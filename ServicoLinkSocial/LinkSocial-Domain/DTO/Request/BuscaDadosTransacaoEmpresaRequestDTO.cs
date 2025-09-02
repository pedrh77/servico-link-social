using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.DTO.Request
{
    public class BuscaDadosTransacaoEmpresaRequestDTO
    {
        public int EmpresaId { get; set; }
        public StatusPagamento? StatusPagamento{ get; set; }
    }
}
