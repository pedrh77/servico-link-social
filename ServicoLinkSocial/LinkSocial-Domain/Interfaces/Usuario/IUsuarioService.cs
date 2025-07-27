using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Usuarios
{
    public interface IUsuarioService
    {
        Task<bool> AtualizarUsuario(int id, NovoUsuarioRequestDTO request);
        Task<bool> DeletarUsuario(int id);
        Task<UsuarioResponseDTO> ObterPorId(int id);
        Task<List<UsuarioResponseDTO>> ObterPorTipo(int tipo);
        Task<List<UsuarioResponseDTO>> ObterTodos();
        Task RegistraUsuario(NovoUsuarioRequestDTO request);
        Task<Usuario> ValidaDadosLoginUsario(LoginRequestDTO login);
    }
}
