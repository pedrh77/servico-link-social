using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Usuarios
{
    public interface IUsuarioService
    {
        Task AtualizarUsuario(int id, AtualizaDadosUsuarioRequestDTO request);
        Task<bool> DeletarUsuario(int id);
        Task<Usuario> ObterPorId(int id);
        Task<List<UsuarioResponseDTO>> ObterPorTipo(TipoUsuario tipo);
        Task<List<UsuarioResponseDTO>> ObterTodos();
        Task RegistraUsuario(NovoUsuarioRequestDTO request);
        Task<Usuario> ValidaDadosLoginUsario(LoginRequestDTO login);
    }
}
