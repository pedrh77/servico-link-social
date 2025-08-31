using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Interfaces.Beneficios;
using Microsoft.AspNetCore.Mvc;

namespace LinkSocial_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BeneficiosController(IBeneficioService _beneficioService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegistraBeneficioOng([FromBody] NovoBeneficioRequestDTO request)
        {
            try
            {
                await _beneficioService.CadastrarBeneficioAsync(request);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscaBeneficios()
        {
            try
            {
                return Ok(await _beneficioService.ListarBeneficiosAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscaBeneficioPorId(int id)
        {
            try
            {
                var beneficio = await _beneficioService.ObterBeneficioPorIdAsync(id);
                if (beneficio == null)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpGet("Usuario/{id}")]
        public async Task<IActionResult> BuscaBeneficioPorUsuarioId(int id)
        {
            try
            {
                var beneficio = await _beneficioService.BuscaBeneficioPorUsuarioIdAsync(id);
                if (beneficio == null)
                    return NotFound();

                return Ok(beneficio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarBeneficio(int id)
        {
            try
            {
                var sucesso = await _beneficioService.DeletarBeneficioAsync(id);
                if (!sucesso)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }
    }
}
