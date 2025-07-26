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
            await _context.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidaCPFExistente(string? cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            return await _context.Usuarios
                .AnyAsync(x => x.Cpf == cpf);
        }

        public async Task<bool> ValidaEmailExistente(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _context.Usuarios
                .AnyAsync(x => x.Email.ToUpper() == email.ToUpper());
        }

        public async Task<Usuario> ValidaUsuarioLogin(string email, string senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x =>
            x.Email.ToUpper().Equals(email.ToUpper()) &&
            x.SenhaHash.Equals(senha) &&
            x.Ativo == true);
        }
    }
}
