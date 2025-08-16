using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Doacoes;
using LinkSocial_Domain.Models;
using LinkSocial_Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkSocial_Infra.Repository
{
    public class DoacaoRepository : IDoacaoRepository
    {
        private readonly LinkSocialDbContext _context;

        public DoacaoRepository(LinkSocialDbContext context)
        {
            _context = context;
        }

        public async Task<Doacao> AdicionarAsync(Doacao doacao)
        {
            _context.Doacoes.Add(doacao);
            await _context.SaveChangesAsync();
            return doacao;
        }

        public async Task<Doacao?> ObterPorIdAsync(int id)
        {
            return await _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.Ong)
                .Include(d => d.Beneficio)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Doacao>> ObterTodasAsync()
        {
            return await _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.Ong)
                .Include(d => d.Beneficio)
                .OrderByDescending(d => d.Criado_em)
                .ToListAsync();
        }

        public async Task<List<Doacao>> ObterPorDoadorAsync(int doadorId)
        {
            return await _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.Ong)
                .Include(d => d.Beneficio)
                .Where(d => d.DoadorId == doadorId)
                .OrderByDescending(d => d.Criado_em)
                .ToListAsync();
        }

        public async Task<List<Doacao>> ObterPorBeneficioAsync(int beneficioId)
        {
            return await _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.Ong)
                .Include(d => d.Beneficio)
                .Where(d => d.BeneficioId == beneficioId)
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
                .Include(d => d.Beneficio)
                .Where(d => d.OngId == ongId)
                .OrderByDescending(d => d.Criado_em)
                .ToListAsync();
        }

        public async Task<List<Doacao>> ObterValoresArrecadadosporBeneficio(int id)
        {
            return await _context.Doacoes.Where(d => (d.StatusPagamento == StatusPagamento.Aprovado || d.StatusPagamento == StatusPagamento.Pago) && d.BeneficioId == id && d.Deleted == false)
                .ToListAsync();
        }
    }
}