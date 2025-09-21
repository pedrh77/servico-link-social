using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Carteiras;
using LinkSocial_Domain.Interfaces.Pedidos;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class CarteiraService(ICarteiraRepository _carteiraRepository, IUsuarioRepository _usuarioRepository, IPedidoService _pedidoService, IMapper _mapper) : ICarteiraService
    {



        public async Task AdicionarTransacao(NovaTransacaoRequestDTO request)
        {
            // 1️⃣ Busca a carteira do doador
            var carteira = await _carteiraRepository.BuscaCarteiraDoador(request.DoadorId);
            if (carteira == null)
                throw new ArgumentException("Carteira do doador não encontrada.");

            if (request.EmpresaId != null)
            {
                var empresa = await _usuarioRepository.ObterPorId(request.EmpresaId.Value);
                if (empresa == null || empresa.TipoUsuario != TipoUsuario.Empresa)
                    throw new Exception("AdicionarTransacao: Usuário não é empresa ou empresa não existe.");
            }

            var transacao = _mapper.Map<Transacao>(request);
            transacao.Criado_em = DateTime.UtcNow;
            transacao.CarteiraId = carteira.Id;
            transacao.Status = StatusPagamento.Pendente;

            if (request.Tipo == TipoTransacao.Debito)
            {
                transacao.NomeTransacao = _pedidoService.GerarCodigo(3);
                if (request.ValorTotal < request.Valor * 2)
                    throw new Exception($"Transação não permitida. O ValorTotal deve ser pelo menos {request.Valor * 2}.");
            }

            carteira.RegistrarTransacao(transacao);

            await _carteiraRepository.AdicionaTransacao(transacao);
            await _carteiraRepository.AtualizaCarteira(carteira);

            if (request.Tipo == TipoTransacao.Debito)
            {
                await _pedidoService.GerarPedido(transacao.Id, transacao.ReceiverId, request.DoadorId);
            }
        }


        public async Task<bool> AtualizaStatusCarteira(int transacaoId, int clientId, StatusPagamento novoStatus)
        {
            var transacao = await _carteiraRepository.BuscaTransacaoById(transacaoId);
            if (transacao == null)
                throw new Exception("Transação não encontrada.");

            var carteira = await _carteiraRepository.BuscaCarteiraDoador(clientId);
            if (carteira == null)
                throw new Exception("Carteira não encontrada.");

            if (novoStatus == StatusPagamento.Aprovado)
            {
                if (transacao.Tipo == TipoTransacao.Debito && carteira.SaldoAprovado < transacao.Valor)
                {
                    transacao.Status = StatusPagamento.Rejeitado;
                    await _carteiraRepository.AtualizaCarteira(carteira);
                    return false;
                }

                carteira.AprovarTransacao(transacao);
                await _carteiraRepository.AtualizaCarteira(carteira);
                return true;
            }
            else if (novoStatus == StatusPagamento.Rejeitado)
            {
                carteira.RejeitarTransacao(transacao);
                await _carteiraRepository.AtualizaCarteira(carteira);
                return false;
            }

            return false;
        }



        public async Task<CarteiraResponseDTO> BuscarCarteiraPorUsuarioId(int id)
        {
            var carteira = await _carteiraRepository.BuscaCarteiraDoador(id);
            carteira.ExpirarTransacoesPendentes();
            await _carteiraRepository.AtualizaCarteira(carteira);
            if (carteira == null)
                throw new Exception("Carteira não encontrada para o usuário.");
            return _mapper.Map<CarteiraResponseDTO>(carteira);

        }

        public async Task<List<TransacaoResponseDTO>> BuscarCarteiraUsuarioStatus(int usuarioId, StatusPagamento? status)
        {
            var transacoes = await _carteiraRepository.BuscaTransacoesByIdAndStatus(usuarioId, status);
            return _mapper.Map<List<TransacaoResponseDTO>>(transacoes);
        }

        public async Task<List<TransacaoResponseDTO>> BuscarTransacoesRecebidas(int empresaId, StatusPagamento? status)
        {
            var transacoes = await _carteiraRepository.BuscarTransacoesRecebidas(empresaId, status);
            return _mapper.Map<List<TransacaoResponseDTO>>(transacoes);
        }

        public async Task GerarCarteiraUsuario(int id)
        {
            var carteira = new Carteira
            {
                UsuarioId = id
            };
            await _carteiraRepository.AdicionaCarteiraDoador(carteira);

        }

    }
}
