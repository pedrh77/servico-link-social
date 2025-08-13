using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;

namespace LinkSocial_Domain.Interfaces.Beneficios
{
    public interface IBeneficioService
    {
        Task<List<BeneficioResponseDTO>> BuscaBeneficioPorUsuarioIdAsync(int id);
        Task CadastrarBeneficioAsync(NovoBeneficioRequestDTO request);
        Task<bool> DeletarBeneficioAsync(int id);
        Task<List<BeneficioResponseDTO>> ListarBeneficiosAsync();
        Task<BeneficioResponseDTO?> ObterBeneficioPorIdAsync(int id);
    }
}
