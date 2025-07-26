using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Usuarios
{
    public interface IUsuarioService
    {
        Task RegistraUsuario(NovoUsuarioRequestDTO request);
        Task<Usuario> ValidaDadosLoginUsario(LoginRequestDTO login);
    }
}
