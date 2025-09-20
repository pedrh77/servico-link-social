using LinkSocial_Domain.Enum;
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
            return await _context.Pedidos.Include(d => d.Transacao).FirstOrDefaultAsync(d => d.TransacaoId == id && d.Deleted == false);
        }


        public async Task<List<Pedido>> BuscaPedidosEmpresaId(int id, StatusPagamento? status)
        {
            if (status == null)
                return await _context.Pedidos.Include(x => x.Transacao).Include(x=>x.Doador).Where(x => x.EmpresaId == id && x.Transacao.Status == StatusPagamento.Pendente && x.Deleted == false).ToListAsync();
            return await _context.Pedidos.Include(x => x.Transacao).Where(x => x.EmpresaId == id && x.Transacao.Status == status && x.Deleted == false).ToListAsync();
        }
    }
}
