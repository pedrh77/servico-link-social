using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Carteiras;
using LinkSocial_Domain.Models;
using LinkSocial_Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkSocial_Infra.Repository
{
    public class CarteiraRepository(LinkSocialDbContext _context) : ICarteiraRepository
    {
        public async Task AdicionaCarteiraDoador(Carteira carteira)
        {
            carteira.Criado_em = DateTime.UtcNow;

            await _context.Carteiras.AddAsync(carteira);
            await _context.SaveChangesAsync();

        }

        public async Task AdicionaTransacao(Transacao transacaoDoador)
        {
            await _context.Transacoes.AddAsync(transacaoDoador);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizaCarteira(Carteira carteiradoador)
        {
            _context.Carteiras.Update(carteiradoador);
            await _context.SaveChangesAsync();
        }

        public async Task<Carteira> BuscaCarteiraDoador(int doadorid)
        {
            return await _context.Carteiras.Where(c => c.UsuarioId == doadorid && c.Deleted == false).Include(c => c.Transacoes).FirstOrDefaultAsync();
        }

        public async Task<List<Transacao>> BuscarTransacoesRecebidas(int empresaId, StatusPagamento? status)
        {
           if(status!= null)
                return await _context.Transacoes.Where(c => c.ReceiverId ==empresaId && status.Equals(c.Status) && c.Deleted == false).ToListAsync();
            return await _context.Transacoes.Where(c => c.ReceiverId == empresaId && c.Deleted == false).ToListAsync();
        }
    }
}
