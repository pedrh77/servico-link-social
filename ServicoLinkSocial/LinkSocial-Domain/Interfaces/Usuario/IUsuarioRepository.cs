
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Usuarios
{
    public interface IUsuarioRepository
    {
        Task Save(Usuario usuario);
        Task<bool> ValidaCPFExistente(string? cpf);
        Task<bool> ValidaEmailExistente(string email);
        Task<Usuario> ValidaUsuarioLogin(string email, string senhaCoded);
    }
}
