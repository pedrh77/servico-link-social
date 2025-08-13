using LinkSocial_Domain.Interfaces.Beneficios;
using LinkSocial_Domain.Models;
using LinkSocial_Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkSocial_Infra.Repository
{
    public class BeneficioRepository : IBeneficioRepository
    {
        private readonly LinkSocialDbContext _context;

        public BeneficioRepository(LinkSocialDbContext context)
        {
            _context = context;
        }

        public async Task<Beneficio> ObterPorId(int id)
        {
            return await _context.Beneficos.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Beneficio>> ObterTodos()
        {
            return await _context.Beneficos
                .Include(b => b.Usuario)
                .OrderByDescending(b => b.Criado_em)
                .ToListAsync();
        }

        public async Task Save(Beneficio beneficio)
        {
            if (beneficio.Id == 0)
            {
                _context.Add(beneficio);
            }
            else
            {
                _context.Beneficos.Update(beneficio);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(object beneficio)
        {
            if (beneficio is Beneficio beneficioEntity)
            {
                _context.Beneficos.Remove(beneficioEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Beneficio>> ObterBeneficioPorUsuarioId(int id)
        {
            return await _context.Beneficos.Where(x => x.UsuarioId == id && x.Deleted == false).ToListAsync();
        }

        public async Task<List<Beneficio>> ObterPorUsuarioId(int id)
        {
            throw new NotImplementedException();
        }
    }
}