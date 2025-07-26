using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Interfaces;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class UsuarioService(IUsuarioRepository _usuarioRepository, IMapper _mapper, IMd5HashService _md5HashService) : IUsuarioService
    {
        public async Task RegistraUsuario(NovoUsuarioRequestDTO request)
        {
            if (await _usuarioRepository.ValidaCPFExistente(request.Cpf))
                throw new InvalidOperationException("CPF já cadastrado.");

            if (await _usuarioRepository.ValidaEmailExistente(request.Email))
                throw new InvalidOperationException("E-mail já cadastrado.");

            var usuario = _mapper.Map<Usuario>(request);

            usuario.SenhaHash = await _md5HashService.GerarHashMd5(request.Senha);

            await _usuarioRepository.Save(usuario);
        }

        public async Task<Usuario> ValidaDadosLoginUsario(LoginRequestDTO login)
        {
            var senha = await _md5HashService.GerarHashMd5(login.Senha);

            return await _usuarioRepository.ValidaUsuarioLogin(login.Email, senha);
        }
    }
}
