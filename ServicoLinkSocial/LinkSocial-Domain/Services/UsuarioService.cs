using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        public Task<Usuario> ValidaDadosLoginUsario(LoginRequestDTO login)
        {
            throw new NotImplementedException();
        }
    }
}
