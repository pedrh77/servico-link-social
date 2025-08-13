using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Beneficios
{
    public interface IBeneficioRepository
    {
        Task Deletar(object beneficio);
        Task<List<Beneficio>> ObterBeneficioPorUsuarioId(int id);
        Task<Beneficio> ObterPorId(int id);
        Task<List<Beneficio>> ObterPorUsuarioId(int id);
        Task <List<Beneficio>>ObterTodos();
        Task Save(Beneficio beneficio);
    }
}
