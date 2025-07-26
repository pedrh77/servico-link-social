using LinkSocial_Domain.DTO.Request;
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
        public async Task<IActionResult> RegistraUsuario(NovoUsuarioRequestDTO request)
        {
            await _usuarioService.RegistraUsuario(request);
            return Created();
        }


    }
}
