using LinkSocial_Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkSocial_Domain.DTO.Response
{
    public class PedidosValidacaoResponseDTO
    {
        public string Codigo { get; set; }
        public int TransacaoId { get; set; }
        public string NomeDoador { get; set; }
        public decimal Valor { get; set; }
        public StatusPagamento Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
