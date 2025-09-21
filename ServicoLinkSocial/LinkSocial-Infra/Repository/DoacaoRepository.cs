using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Doacoes;
using LinkSocial_Domain.Models;
using LinkSocial_Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkSocial_Infra.Repository
{
    public class DoacaoRepository(LinkSocialDbContext _context) : IDoacaoRepository
    {

        public async Task<Doacao> AdicionarAsync(Doacao doacao)
        {
            _context.Doacoes.Add(doacao);
            await _context.SaveChangesAsync();
            return doacao;
        }

        public async Task<Doacao?> ObterPorIdAsync(int id)
        {
            return await _context.Doacoes
                .Include(d=>d.Parcelas)
                .Include(d => d.Doador)
                .Include(d => d.Ong)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Doacao>> ObterTodasAsync()
        {
            return await _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.Ong)
                .OrderByDescending(d => d.Criado_em)
                .ToListAsync();
        }

        public async Task<List<Doacao>> ObterPorDoadorAsync(int doadorId)
        {
            return await _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.Ong)
                .Where(d => d.DoadorId == doadorId)
                .OrderByDescending(d => d.Criado_em)
                .ToListAsync();
        }



        public async Task<bool> AtualizarAsync(Doacao doacao)
        {
            _context.Doacoes.Update(doacao);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var doacao = await _context.Doacoes.FindAsync(id);
            if (doacao == null) return false;

            _context.Doacoes.Remove(doacao);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> AtualizarStatusPagamentoAsync(int id, StatusPagamento status)
        {
            var doacao = await _context.Doacoes.FindAsync(id);
            if (doacao == null) return false;

            doacao.StatusPagamento = status;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Doacao>> ObterPorOngAsync(int ongId)
        {
            return await _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.Ong)
                .Where(d => d.OngId == ongId)
                .OrderByDescending(d => d.Criado_em)
                .ToListAsync();
        }

        public async Task<Doacao> BuscaDoacaoPorValorUsuario(int doadorId, decimal valor, int ongId)
        {
            return await _context.Doacoes.Where(x => x.DoadorId == doadorId
               && x.Valor == valor && x.OngId == ongId)
      .OrderByDescending(x => x.Modificado_em ).FirstOrDefaultAsync();
        }
    }
}