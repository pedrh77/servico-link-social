using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Pedidos;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class PedidoService(IPedidoRepository _pedidoRepository, IMapper _mapper) : IPedidoService
    {

        public async Task<List<PedidosValidacaoResponseDTO>> BuscaPedidosValidacao(int id, StatusPagamento? status)
        {
            List<Pedido> pedidos = await _pedidoRepository.BuscaPedidosEmpresaId(id, status);
            return _mapper.Map<List<PedidosValidacaoResponseDTO>>(pedidos);
        }

        public string GerarCodigo(int tamanho)
        {
            Random _random = new Random();
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(caracteres, tamanho)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }


        public async Task GerarPedido(int transacaoId, int? empresaId, int doadorId)
        {
            var pedido = new Pedido
            {
                Codigo = GerarCodigo(6),
                TransacaoId = transacaoId,
                EmpresaId = empresaId,
                Status = Enum.StatusPagamento.Pendente,
                Criado_em = DateTime.UtcNow,
                DoadorId = doadorId
            };

            await _pedidoRepository.AdicionaPedidoPendente(pedido);
        }

        public async Task ValidarTransacaoCodigoUsuario(int id, PedidoValidacaoRequestDTO request, bool aprovado)
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

            if (aprovado)
            {
                pedido.Status = Enum.StatusPagamento.Aprovado;
                pedido.Transacao.Status = Enum.StatusPagamento.Aprovado;
                pedido.Modificado_em = DateTime.UtcNow;
            }
            else { pedido.Status = StatusPagamento.Rejeitado;
                pedido.Modificado_em = DateTime.UtcNow; }

            await _pedidoRepository.AtualizaPedido(pedido);
        }
    }
}
