using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Interfaces.Pedidos
{
    public interface IPedidoService
    {
        string GerarCodigo();
        Task ValidarTransacaoCodigoUsuario(int id, PedidoValidacaoRequestDTO request);
        Task GerarPedido(int transacaoId, int? empresaId, int doadorId);
        Task<List<PedidosValidacaoResponseDTO>> BuscaPedidosValidacao(int id,StatusPagamento? status);
    }
}
