
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Usuarios
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPorId(int id);
        Task<List<Usuario>> ObterPorTipo(TipoUsuario tipo);
        Task<List<Usuario>> ObterTodos();
        Task Save(Usuario usuario);
        Task<bool> ValidaCNPJExistente(string? cnpj);
        Task<bool> ValidaCPFExistente(string? cpf);
        Task<bool> ValidaEmailExistente(string email);
        Task<Usuario> ValidaUsuarioLogin(string email, string senhaCoded);
    }
}
