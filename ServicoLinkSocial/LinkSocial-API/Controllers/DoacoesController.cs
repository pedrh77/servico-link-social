using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Doacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkSocial_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DoacoesController(IDoacaoService _doacaoService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegistrarDoacao([FromBody] NovaDoacaoRequestDTO request)
        {
            try
            {
                var doacao = await _doacaoService.CadastrarDoacaoAsync(request);
                return CreatedAtAction(nameof(ObterPorId), new { id = doacao.Id }, doacao);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodasDoacoes()
        {
            try
            {
                var doacoes = await _doacaoService.ObterTodasAsync();
                return Ok(doacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            try
            {
                var doacao = await _doacaoService.ObterPorIdAsync(id);
                if (doacao == null)
                    return NotFound();

                return Ok(doacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpGet("doador/{doadorId}")]
        [Authorize(Roles = "Doador")]
        public async Task<IActionResult> BuscarPorDoador(int doadorId)
        {
            try
            {
                var doacoes = await _doacaoService.ObterPorDoadorAsync(doadorId);
                return Ok(doacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpGet("beneficio/{beneficioId}")]
        public async Task<IActionResult> BuscarPorBeneficio(int beneficioId)
        {
            try
            {
                var doacoes = await _doacaoService.ObterPorBeneficioAsync(beneficioId);
                return Ok(doacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpGet("ong/{ongId}")]
        public async Task<IActionResult> BuscarPorOng(int ongId)
        {
            try
            {
                var doacoes = await _doacaoService.ObterPorOngAsync(ongId);
                return Ok(doacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarDoacao(int id, [FromBody] NovaDoacaoRequestDTO request)
        {
            try
            {
                var sucesso = await _doacaoService.AtualizarDoacaoAsync(id, request);
                if (!sucesso)
                    return NotFound();

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarDoacao(int id)
        {
            try
            {
                var sucesso = await _doacaoService.DeletarDoacaoAsync(id);
                if (!sucesso)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatusPagamento(int id, [FromBody] StatusPagamento status)
        {
            try
            {
                var sucesso = await _doacaoService.AtualizarStatusPagamentoAsync(id, status);
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