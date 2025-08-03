using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class UsuarioService(IUsuarioRepository _usuarioRepository, IMapper _mapper, IMd5HashService _md5HashService) : IUsuarioService
    {
        public async Task<bool> AtualizarUsuario(int id, NovoUsuarioRequestDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletarUsuario(int id)
        {
            var usuario = await _usuarioRepository.ObterPorId(id);
            if (usuario == null)
                return false;

            usuario.Deleted = true;
            usuario.Ativo = false;
            usuario.Modificado_em = DateTime.UtcNow;

            await _usuarioRepository.Save(usuario);
            return true;
        }

        public async Task<Usuario> ObterPorId(int id)
        {
            var usuario = await _usuarioRepository.ObterPorId(id);
            if (usuario == null)
                return null;

            return usuario;
        }

        public async Task<List<UsuarioResponseDTO>> ObterPorTipo(TipoUsuario tipo)
        {
            var usuarios = await _usuarioRepository.ObterPorTipo(tipo);
            return _mapper.Map<List<UsuarioResponseDTO>>(usuarios);
        }

        public async Task<List<UsuarioResponseDTO>> ObterTodos()
        {
            var usuarios = await _usuarioRepository.ObterTodos();
            return _mapper.Map<List<UsuarioResponseDTO>>(usuarios);
        }
        public async Task RegistraUsuario(NovoUsuarioRequestDTO request)
        {
            if (request.TipoUsuario == TipoUsuario.Doador)
            {
                if (await _usuarioRepository.ValidaCPFExistente(request.Cpf))
                    throw new InvalidOperationException("CPF já cadastrado.");
            }
            else
            {
                if (await _usuarioRepository.ValidaCNPJExistente(request.Cnpj))
                    throw new InvalidOperationException("CNPJ já cadastrado.");
            }

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
