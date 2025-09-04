using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Doacoes
{
    public interface IDoacaoRepository
    {
        Task<Doacao> AdicionarAsync(Doacao doacao);
        Task<Doacao?> ObterPorIdAsync(int id);
        Task<List<Doacao>> ObterTodasAsync();
        Task<List<Doacao>> ObterPorDoadorAsync(int doadorId);
        Task<bool> AtualizarAsync(Doacao doacao);
        Task<bool> DeletarAsync(int id);
        Task<bool> AtualizarStatusPagamentoAsync(int id, StatusPagamento status);
        Task<List<Doacao>> ObterPorOngAsync(int ongId);
        Task<Doacao> BuscaDoacaoPorValorUsuario(int doadorId, decimal valor, int ongId);
    }
} 