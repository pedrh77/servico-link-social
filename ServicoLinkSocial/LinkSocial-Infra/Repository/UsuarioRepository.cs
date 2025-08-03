using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Models;
using LinkSocial_Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkSocial_Infra.Repository
{
    public class UsuarioRepository(LinkSocialDbContext _context) : IUsuarioRepository
    {

        public async Task Save(Usuario usuario)
        {
            if (usuario.Id == 0)
            {
                await _context.Usuarios.AddAsync(usuario);
            }
            else
            {
                _context.Usuarios.Update(usuario);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidaCPFExistente(string? cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            return await _context.Usuarios.AnyAsync(u => u.Cpf == cpf && !u.Deleted);
        }

        public async Task<bool> ValidaEmailExistente(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email && !u.Deleted);
        }

        public async Task<Usuario> ValidaUsuarioLogin(string email, string senhaCoded)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.SenhaHash == senhaCoded && !u.Deleted && u.Ativo);
        }


        public async Task<Usuario> ObterPorId(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id && !u.Deleted);
        }

        public async Task<List<Usuario>> ObterPorTipo(TipoUsuario tipo)
        {
            return await _context.Usuarios
                .Where(u => u.TipoUsuario == tipo && !u.Deleted)
                .ToListAsync();
        }

        public async Task<List<Usuario>> ObterTodos()
        {
            return await _context.Usuarios
                .Where(u => !u.Deleted)
                .ToListAsync();
        }

    }
}
