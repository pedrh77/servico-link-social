using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Models
{
    public class Pedido :EntityBase
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int TransacaoId { get; set; }
        public Transacao Transacao { get; set; }
        public int? EmpresaId { get; set; }
        public Usuario Empresa { get; set; }
        public StatusPagamento Status { get; set; }
        
        public int DoadorId { get; set; }
        public Usuario Doador { get; set; }
    }
}
