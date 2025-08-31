using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Interfaces.Beneficios;
using LinkSocial_Domain.Interfaces.Doacoes;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class BeneficioService(
        IBeneficioRepository _beneficioRepository,
        IDoacaoRepository _doacaoRepository,
        IUsuarioService _usuarioService,
        IMapper _mapper) : IBeneficioService
    {
        public async Task<List<BeneficioResponseDTO>> BuscaBeneficioPorUsuarioIdAsync(int id)
        {
            var usuario = await _usuarioService.ObterPorId(id);
            if (usuario == null || !usuario.Ativo)
                throw new Exception("ONG não encontrada ou inativa.");


            if (usuario.TipoUsuario != Enum.TipoUsuario.ONG)
                throw new Exception("Usuário informado não é uma ONG.");

            List<Beneficio> beneficios = await _beneficioRepository.ObterBeneficioPorUsuarioId(id);
            if (beneficios == null || beneficios.Count < 1)
                throw new Exception("Nenhum benefício encontrado para o usuário informado.");
            var mapped = _mapper.Map<List<BeneficioResponseDTO>>(beneficios);
            return mapped;

        }

        public async Task CadastrarBeneficioAsync(NovoBeneficioRequestDTO request)
        {
            var usuario = await _usuarioService.ObterPorId(request.UsuarioId);
            if (usuario == null || !usuario.Ativo)
                throw new Exception("ONG não encontrada ou inativa.");

            if (usuario.TipoUsuario != Enum.TipoUsuario.ONG)
                throw new Exception("Usuário informado não é uma ONG.");

            var beneficio = _mapper.Map<Beneficio>(request);
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
