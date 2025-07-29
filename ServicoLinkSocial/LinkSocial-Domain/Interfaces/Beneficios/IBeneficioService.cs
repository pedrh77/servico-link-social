using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Beneficios
{
    public interface IBeneficioService
    {
        Task CadastrarBeneficioAsync(BeneficiosRequestDTO request);
        Task<bool> DeletarBeneficioAsync(int id);
        Task ListarBeneficiosAsync();
        Task<List<Beneficio>> ObterBeneficioPorIdAsync(int id);
    }
}
