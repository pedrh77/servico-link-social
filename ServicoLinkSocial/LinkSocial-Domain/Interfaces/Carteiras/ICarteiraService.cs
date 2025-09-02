
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Carteiras
{
    public interface ICarteiraService
    {

        Task AdicionarTransacao(NovaTransacaoRequestDTO request);
        Task<Carteira> BuscarCarteiraPorUsuarioId(int id);
        Task<List<TransacaoResponseDTO>> BuscarTransacoesRecebidas(int empresaId,StatusPagamento? status);
        Task GerarCarteiraUsuario(int id);
    }
}
