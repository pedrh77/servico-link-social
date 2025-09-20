using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Carteiras;
using LinkSocial_Domain.Interfaces.Pedidos;
using Microsoft.AspNetCore.Mvc;

namespace LinkSocial_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CarteiraController : ControllerBase
    {
        private readonly ICarteiraService _carteiraService;
        private readonly IPedidoService _pedidoService;

        public CarteiraController(ICarteiraService carteiraService, IPedidoService pedidoService)
        {
            _carteiraService = carteiraService;
            _pedidoService = pedidoService;
        }

        [HttpPost("Transacao")]
        public async Task<IActionResult> RealizaTransacao(NovaTransacaoRequestDTO request)
        {
            await _carteiraService.AdicionarTransacao(request);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransacaoByUsuarioId(int id)
        {
            var carteira = await _carteiraService.BuscarCarteiraPorUsuarioId(id);
            return Ok(carteira);
        }

        [HttpGet("Transacao/Recebida/")]
        public async Task<IActionResult> GetTransacaoRecebidas([FromQuery] int EmpresaId, [FromQuery] StatusPagamento? Status)
        {
            var pedidos = await _pedidoService.BuscaPedidosValidacao(EmpresaId, Status);
            return Ok(pedidos);
        }

        [HttpGet("Transacoes/Enviadas")]
        public async Task<IActionResult> BuscarCarteiraUsuarioStatus([FromQuery] int UsuarioId, [FromQuery] StatusPagamento? Status)
        {
            var transacoes = await _carteiraService.BuscarCarteiraUsuarioStatus(UsuarioId, Status);
            return Ok(transacoes);
        }

        [HttpPost("Transacao/{id}/Aprovacao")]
        public async Task<IActionResult> AprovaTransacao(int id, PedidoValidacaoRequestDTO request)
        {
            await _pedidoService.ValidarTransacaoCodigoUsuario(id, request);
            await _carteiraService.AtualizaStatusCarteira(id, request.ClienteId, request.NovoStatus);
            return Ok();
        }

    }
}
