using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Usuarios
{
    public interface IUsuarioService
    {
        Task<Usuario> ValidaDadosLoginUsario(LoginRequestDTO login);
    }
}
