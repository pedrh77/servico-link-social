using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Carteiras;
using LinkSocial_Domain.Interfaces.Doacoes;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class DoacaoService(IDoacaoRepository _doacaoRepository, IUsuarioService _usuarioService, IMapper _mapper, ICarteiraService _carteira) : IDoacaoService
    {

        public async Task<DoacaoResponseDTO> CadastrarDoacaoAsync(NovaDoacaoRequestDTO request)
        {
            var doador = await _usuarioService.ObterPorId(request.DoadorId);
            if (doador == null)
                throw new ArgumentException("Doador não encontrado.");

            var ong = await _usuarioService.ObterPorId(request.OngId);
            if (ong == null)
                throw new ArgumentException("ONG não encontrada.");

            var doacao = _mapper.Map<Doacao>(request);

            if (request.PagamentoParcela == true)
            {



                var doacaoExistente = await _doacaoRepository.ObterPorIdAsync(request.PrimeiraDoacao.Value);

                if (doacaoExistente == null)
                    throw new ArgumentException("Nenhuma doação encontrada para continuar o parcelamento.");


                int proximaParcela = doacaoExistente.NumeroParcela + 1;
                if (proximaParcela > doacaoExistente.TotalParcelas)
                    throw new Exception("Todas as parcelas já foram pagas.");

                doacao = new Doacao
                {
                    DoadorId = request.DoadorId,
                    OngId = request.OngId,
                    Valor = request.Valor,
                    StatusPagamento = StatusPagamento.Pendente,
                    TotalParcelas = doacaoExistente.TotalParcelas,
                    NumeroParcela = proximaParcela,
                    Criado_em = DateTime.UtcNow
                };

                doacaoExistente.Parcelas.Add(doacao);
            }
            else
            {
                int totalParcelas = request.TipoDoacao switch
                {
                    TipoDoacao.Unica => 1,
                    TipoDoacao.Mensal6x => 6,
                    TipoDoacao.Mensal12x => 12
                };

                doacao = _mapper.Map<Doacao>(request);
                doacao.StatusPagamento = StatusPagamento.Pendente;
                doacao.TotalParcelas = totalParcelas;
                doacao.NumeroParcela = 1;
                doacao.Criado_em = DateTime.UtcNow;
            }

            var doacaoCriada = await _doacaoRepository.AdicionarAsync(doacao);

            var transacao = new NovaTransacaoRequestDTO()
            {
                DoadorId = doador.Id,
                Valor = doacao.Valor * 2,
                Tipo = TipoTransacao.Credito,
                EmpresaId = null,
            };


            await _carteira.AdicionarTransacao(transacao);
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



        public async Task<bool> AtualizarDoacaoAsync(int id, NovaDoacaoRequestDTO request)
        {
            var doacao = await _doacaoRepository.ObterPorIdAsync(id);
            if (doacao == null) return false;

            var doador = await _usuarioService.ObterPorId(request.DoadorId);
            if (doador == null)
                throw new ArgumentException("Doador não encontrado.");

            var ong = await _usuarioService.ObterPorId(request.OngId);
            if (ong == null)
                throw new ArgumentException("ONG não encontrada.");


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