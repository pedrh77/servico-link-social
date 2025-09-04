using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Interfaces.Doacoes
{
    public interface IDoacaoService
    {
        Task<DoacaoResponseDTO> CadastrarDoacaoAsync(NovaDoacaoRequestDTO request);
        Task<DoacaoResponseDTO?> ObterPorIdAsync(int id);
        Task<List<DoacaoResponseDTO>> ObterTodasAsync();
        Task<List<DoacaoResponseDTO>> ObterPorDoadorAsync(int doadorId);
        Task<bool> AtualizarDoacaoAsync(int id, NovaDoacaoRequestDTO request);
        Task<bool> DeletarDoacaoAsync(int id);
        Task<bool> AtualizarStatusPagamentoAsync(int id, StatusPagamento status);
        Task<List<DoacaoResponseDTO>> ObterPorOngAsync(int ongId);
    }
} 