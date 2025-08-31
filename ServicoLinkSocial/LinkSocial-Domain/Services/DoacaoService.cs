using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Beneficios;
using LinkSocial_Domain.Interfaces.Carteiras;
using LinkSocial_Domain.Interfaces.Doacoes;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class DoacaoService : IDoacaoService
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IBeneficioService _beneficioService;
        private readonly IMapper _mapper;
        private readonly ICarteiraService _carteira;

        public DoacaoService(
            IDoacaoRepository doacaoRepository,
            IUsuarioService usuarioService,
            IBeneficioService beneficioService,
            IMapper mapper, ICarteiraService carteiraService)
        {
            _doacaoRepository = doacaoRepository;
            _usuarioService = usuarioService;
            _beneficioService = beneficioService;
            _mapper = mapper;
            _carteira = carteiraService;
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

            await _carteira.AdicionarTransacao(doador.Id, null, TipoTransacao.Credito, doacao.Valor * 2);


            return _mapper.Map<DoacaoResponseDTO>(doacao);
        }

        public async Task<DoacaoResponseDTO?> ObterPorIdAsync(int id)
        {
            var doacao = await _doacaoRepository.ObterPorIdAsync(id);
            if (doacao == null) return null;

            var response = _mapper.Map<DoacaoResponseDTO>(doacao);
            return response;
        }

        public async Task<List<DoacaoResponseDTO>> ObterTodasAsync()
        {
            var doacoes = await _doacaoRepository.ObterTodasAsync();
            var response = _mapper.Map<List<DoacaoResponseDTO>>(doacoes);
            return response;
        }

        public async Task<List<DoacaoResponseDTO>> ObterPorDoadorAsync(int doadorId)
        {
            var doacoes = await _doacaoRepository.ObterPorDoadorAsync(doadorId);
            var response = _mapper.Map<List<DoacaoResponseDTO>>(doacoes);
            return response;
        }

        public async Task<List<DoacaoResponseDTO>> ObterPorBeneficioAsync(int beneficioId)
        {
            var doacoes = await _doacaoRepository.ObterPorBeneficioAsync(beneficioId);
            var response = _mapper.Map<List<DoacaoResponseDTO>>(doacoes);
            return response;
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


            var response = _mapper.Map<List<DoacaoResponseDTO>>(doacoes);

            return response;
        }


    }
}