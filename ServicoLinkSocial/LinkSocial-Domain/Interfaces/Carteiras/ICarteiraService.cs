
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Carteiras
{
    public interface ICarteiraService
    {

        Task AdicionarTransacao(NovaTransacaoRequestDTO request);
        Task<bool> AtualizaStatusCarteira(int transacaoId, int clientId, StatusPagamento novoStatus);
        Task<CarteiraResponseDTO> BuscarCarteiraPorUsuarioId(int id);
        Task<List<TransacaoResponseDTO>> BuscarCarteiraUsuarioStatus(int usuarioId, StatusPagamento? status);
        Task<List<TransacaoResponseDTO>> BuscarTransacoesRecebidas(int empresaId,StatusPagamento? status);
        Task GerarCarteiraUsuario(int id);
    }
}
