using LinkSocial_Domain.Interfaces.Pedidos;
using LinkSocial_Domain.Models;
using LinkSocial_Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkSocial_Infra.Repository
{
    public class PedidoRepository(LinkSocialDbContext _context) : IPedidoRepository
    {
        public async Task AdicionaPedidoPendente(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizaPedido(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<Pedido?> BuscaPedidoByIdTransacao(int id)
        {
            return await _context.Pedidos.Include(d=>d.Transacao).FirstOrDefaultAsync(d => d.TransacaoId == id && d.Deleted == false);
        }
    }
}
