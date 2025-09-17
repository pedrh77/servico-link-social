using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Interfaces.Carteiras;
using LinkSocial_Domain.Interfaces.Pedidos;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository pedidoRepository;

        public PedidoService(IPedidoRepository _pedidoRepository)
        {
            pedidoRepository = _pedidoRepository;
        }

        public string GerarCodigo()
        {
            Random _random = new Random();
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(caracteres, 6)
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

            await pedidoRepository.AdicionaPedidoPendente(pedido);
        }

        public async Task ValidarTransacaoCodigoUsuario(int id, PedidoValidacaoRequestDTO request)
        {
            var pedido = await pedidoRepository.BuscaPedidoByIdTransacao(id);
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

            await pedidoRepository.AtualizaPedido(pedido);
        }
    }
}
