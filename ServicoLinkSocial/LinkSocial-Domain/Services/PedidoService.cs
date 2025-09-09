using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Interfaces.Pedidos;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class PedidoService(IPedidoRepository _pedidoRepository) : IPedidoService
    {

        public string GerarCodigo()
        {
            Random _random = new Random();
            const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(letras, 6)
                 .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public async Task GerarPedido(int transacaoId, int? empresaId)
        {
            var pedido = new Pedido
            {
                Codigo = GerarCodigo(),
                TransacaoId = transacaoId,
                EmpresaId = empresaId,
                Status = Enum.StatusPagamento.Pendente,
                Criado_em = DateTime.UtcNow
            };

            await _pedidoRepository.AdicionaPedidoPendente(pedido);
        }

        public async Task ValidarCodigoUsuario(int id, PedidoValidacaoRequestDTO request)
        {
            var pedido = await _pedidoRepository.BuscaPedidoByIdTransacao(id);
            if (pedido == null)
            {
                throw new Exception("Pedido não encontrado");
            }

            if (pedido.Codigo != request.CodigoValidacao.ToUpper())
            {
                throw new Exception("Código inválido");
            }

            pedido.Status = Enum.StatusPagamento.Aprovado;
            pedido.Transacao.Status = Enum.StatusPagamento.Aprovado;
            pedido.Modificado_em = DateTime.UtcNow;

            await _pedidoRepository.AtualizaPedido(pedido);
        }
    }
}
