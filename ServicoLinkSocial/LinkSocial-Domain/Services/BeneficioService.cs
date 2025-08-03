using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Interfaces.Beneficios;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class BeneficioService(
        IBeneficioRepository _beneficioRepository,
        IUsuarioService _usuarioService,
        IMapper _mapper) : IBeneficioService
    {
        public async Task CadastrarBeneficioAsync(NovoBeneficioRequestDTO request)
        {
            var usuario = await _usuarioService.ObterPorId(request.IdUsuario);
            if (usuario == null || !usuario.Ativo)
                throw new Exception("ONG não encontrada ou inativa.");

            if (usuario.TipoUsuario != Enum.TipoUsuario.Ong)
                throw new Exception("Usuário informado não é uma ONG.");

            var beneficio = _mapper.Map<Beneficio>(request);
            beneficio.UsuarioId = request.IdUsuario;
            await _beneficioRepository.Save(beneficio);
        }

        public async Task<bool> DeletarBeneficioAsync(int id)
        {
            var beneficio = await _beneficioRepository.ObterPorId(id);
            if (beneficio == null)
                return false;

            await _beneficioRepository.Deletar(beneficio);
            return true;
        }

        public async Task<List<BeneficioResponseDTO>> ListarBeneficiosAsync()
        {
            var beneficios = await _beneficioRepository.ObterTodos();
            return _mapper.Map<List<BeneficioResponseDTO>>(beneficios);
        }

        public async Task<BeneficioResponseDTO?> ObterBeneficioPorIdAsync(int id)
        {
            var beneficio = await _beneficioRepository.ObterPorId(id);
            if (beneficio == null)
                return null;

            return _mapper.Map<BeneficioResponseDTO>(beneficio);
        }
    }
}
