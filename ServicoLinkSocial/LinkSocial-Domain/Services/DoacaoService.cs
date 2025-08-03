using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Doacoes;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Interfaces.Beneficios;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class DoacaoService : IDoacaoService
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IBeneficioService _beneficioService;
        private readonly IMapper _mapper;

        public DoacaoService(
            IDoacaoRepository doacaoRepository,
            IUsuarioService usuarioService,
            IBeneficioService beneficioService,
            IMapper mapper)
        {
            _doacaoRepository = doacaoRepository;
            _usuarioService = usuarioService;
            _beneficioService = beneficioService;
            _mapper = mapper;
        }

        public async Task<DoacaoResponseDTO> CadastrarDoacaoAsync(NovaDoacaoRequestDTO request)
        {
            // Validar se o doador existe
            var doador = await _usuarioService.ObterPorId(request.DoadorId);
            if (doador == null)
                throw new ArgumentException("Doador não encontrado.");

            // Validar se a ONG existe
            var ong = await _usuarioService.ObterPorId(request.OngId);
            if (ong == null)
                throw new ArgumentException("ONG não encontrada.");

            // Validar se o benefício existe
            var beneficio = await _beneficioService.ObterBeneficioPorIdAsync(request.BeneficioId);
            if (beneficio == null)
                throw new ArgumentException("Benefício não encontrado.");

            var doacao = _mapper.Map<Doacao>(request);
            doacao.StatusPagamento = StatusPagamento.Pendente;

            var doacaoCriada = await _doacaoRepository.AdicionarAsync(doacao);
            return await MapearParaResponseDTO(doacaoCriada);
        }

        public async Task<DoacaoResponseDTO?> ObterPorIdAsync(int id)
        {
            var doacao = await _doacaoRepository.ObterPorIdAsync(id);
            if (doacao == null) return null;

            return await MapearParaResponseDTO(doacao);
        }

        public async Task<List<DoacaoResponseDTO>> ObterTodasAsync()
        {
            var doacoes = await _doacaoRepository.ObterTodasAsync();
            var responseList = new List<DoacaoResponseDTO>();

            foreach (var doacao in doacoes)
            {
                responseList.Add(await MapearParaResponseDTO(doacao));
            }

            return responseList;
        }

        public async Task<List<DoacaoResponseDTO>> ObterPorDoadorAsync(int doadorId)
        {
            var doacoes = await _doacaoRepository.ObterPorDoadorAsync(doadorId);
            var responseList = new List<DoacaoResponseDTO>();

            foreach (var doacao in doacoes)
            {
                responseList.Add(await MapearParaResponseDTO(doacao));
            }

            return responseList;
        }

        public async Task<List<DoacaoResponseDTO>> ObterPorBeneficioAsync(int beneficioId)
        {
            var doacoes = await _doacaoRepository.ObterPorBeneficioAsync(beneficioId);
            var responseList = new List<DoacaoResponseDTO>();

            foreach (var doacao in doacoes)
            {
                responseList.Add(await MapearParaResponseDTO(doacao));
            }

            return responseList;
        }

        public async Task<bool> AtualizarDoacaoAsync(int id, NovaDoacaoRequestDTO request)
        {
            var doacao = await _doacaoRepository.ObterPorIdAsync(id);
            if (doacao == null) return false;

            // Validar se o doador existe
            var doador = await _usuarioService.ObterPorId(request.DoadorId);
            if (doador == null)
                throw new ArgumentException("Doador não encontrado.");

            // Validar se a ONG existe
            var ong = await _usuarioService.ObterPorId(request.OngId);
            if (ong == null)
                throw new ArgumentException("ONG não encontrada.");

            // Validar se o benefício existe
            var beneficio = await _beneficioService.ObterBeneficioPorIdAsync(request.BeneficioId);
            if (beneficio == null)
                throw new ArgumentException("Benefício não encontrado.");

            _mapper.Map(request, doacao);
            return await _doacaoRepository.AtualizarAsync(doacao);
        }

        public async Task<bool> DeletarDoacaoAsync(int id)
        {
            return await _doacaoRepository.DeletarAsync(id);
        }

        public async Task<bool> AtualizarStatusPagamentoAsync(int id, StatusPagamento status)
        {
            return await _doacaoRepository.AtualizarStatusPagamentoAsync(id, status);
        }

        public async Task<List<DoacaoResponseDTO>> ObterPorOngAsync(int ongId)
        {
            var doacoes = await _doacaoRepository.ObterPorOngAsync(ongId);
            var responseList = new List<DoacaoResponseDTO>();

            foreach (var doacao in doacoes)
            {
                responseList.Add(await MapearParaResponseDTO(doacao));
            }

            return responseList;
        }

        private async Task<DoacaoResponseDTO> MapearParaResponseDTO(Doacao doacao)
        {
            var doador = await _usuarioService.ObterPorId(doacao.DoadorId);
            var ong = await _usuarioService.ObterPorId(doacao.OngId);
            var beneficio = await _beneficioService.ObterBeneficioPorIdAsync(doacao.BeneficioId);

            var response = _mapper.Map<DoacaoResponseDTO>(doacao);
            response.NomeDoador = doador?.Nome ?? "Doador não encontrado";
            response.NomeOng = ong?.Nome ?? "ONG não encontrada";
            response.DescricaoBeneficio = beneficio?.Descricao ?? "Benefício não encontrado";

            return response;
        }
    }
} 