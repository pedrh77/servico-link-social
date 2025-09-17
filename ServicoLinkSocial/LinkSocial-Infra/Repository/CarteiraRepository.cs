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
            try
            {
                await _context.Transacoes.AddAsync(transacaoDoador);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
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
            if (status != null)
                return await _context.Transacoes.Where(c => c.ReceiverId == empresaId && status.Equals(c.Status) && c.Tipo == TipoTransacao.Debito && c.Deleted == false).ToListAsync();
            return await _context.Transacoes.Where(c => c.ReceiverId == empresaId && c.Tipo == TipoTransacao.Debito && c.Deleted == false).ToListAsync();
        }

        public Task<Transacao?> BuscaTransacaoById(int transacaoid)
        {
            return _context.Transacoes.FirstOrDefaultAsync(x => x.Id == transacaoid && x.Deleted == false);
        }

        public Task<List<Transacao>> BuscaTransacoesByIdAndStatus(int usuarioId, StatusPagamento? status)
        {
            if (status != null)
                return _context.Transacoes.Include(x => x.Carteira).Where(c => c.Carteira.UsuarioId == usuarioId && c.Status == status && c.Deleted == false).ToListAsync();
            return _context.Transacoes.Include(x => x.Carteira).Where(c => c.Carteira.UsuarioId == usuarioId && c.Deleted == false).ToListAsync();
        }
    }
}
