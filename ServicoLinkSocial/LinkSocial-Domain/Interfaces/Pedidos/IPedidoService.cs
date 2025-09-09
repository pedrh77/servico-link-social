using LinkSocial_Domain.DTO.Request;

namespace LinkSocial_Domain.Interfaces.Pedidos
{
    public interface IPedidoService
    {
        string GerarCodigo();
        Task ValidarCodigoUsuario(int id, PedidoValidacaoRequestDTO request);
        Task GerarPedido(int transacaoId, int? empresaId);
    }
}
