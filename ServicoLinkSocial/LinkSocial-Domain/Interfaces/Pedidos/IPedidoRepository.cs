using LinkSocial_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkSocial_Domain.Interfaces.Pedidos
{
    public interface IPedidoRepository
    {
        Task AdicionaPedidoPendente(Pedido pedido);
        Task AtualizaPedido(Pedido pedido);
        Task<Pedido?> BuscaPedidoByIdTransacao(int id);
    }
}
