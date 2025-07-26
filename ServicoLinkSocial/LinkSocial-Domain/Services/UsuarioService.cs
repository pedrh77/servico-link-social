using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class UsuarioService(IUsuarioRepository _usuarioRepository, IMapper _mapper) : IUsuarioService
    {
        public Task RegistraUsuario(NovoUsuarioRequestDTO request)
        {
            var usuario = _mapper.Map<Usuario>(request);



            throw new NotImplementedException();
        }

        public Task<Usuario> ValidaDadosLoginUsario(LoginRequestDTO login)
        {
            throw new NotImplementedException();
        }
    }
}
