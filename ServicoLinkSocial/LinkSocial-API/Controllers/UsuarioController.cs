using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace LinkSocial_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsuarioController(IUsuarioService _usuarioService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegistraUsuario([FromBody] NovoUsuarioRequestDTO request)
        {
            await _usuarioService.RegistraUsuario(request);
            return Created(string.Empty, null);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> ObterPorId(int id)
        {
            var usuario = await _usuarioService.ObterPorId(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDTO>>> ObterTodos()
        {
            var usuarios = await _usuarioService.ObterTodos();
            return Ok(usuarios);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] NovoUsuarioRequestDTO request)
        {
            var atualizado = await _usuarioService.AtualizarUsuario(id, request);
            if (!atualizado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarUsuario(int id)
        {
            var deletado = await _usuarioService.DeletarUsuario(id);
            if (!deletado)
                return NotFound();

            return NoContent();
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IList<UsuarioResponseDTO>>> ObterPorTipo(TipoUsuario tipo)
        {
            var usuarios = await _usuarioService.ObterPorTipo(tipo);
            return Ok(usuarios);
        }
    }
}
