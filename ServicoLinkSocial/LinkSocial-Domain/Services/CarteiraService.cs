using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Interfaces.Carteiras;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Services
{
    public class CarteiraService(ICarteiraRepository _carteiraRepository) : ICarteiraService
    {




        public async Task AdicionarTransacao(int doadorid, int? ongid, TipoTransacao tipo, decimal valor)
        {
            var carteiradoador = await _carteiraRepository.BuscaCarteiraDoador(doadorid);
            var transacaoDoador = new Transacao
            {
                CarteiraId = carteiradoador.Id,
                Tipo = tipo,
                Valor = valor,
                Criado_em = DateTime.UtcNow,
                ReceiverId = ongid
            };




            carteiradoador.RegistrarTransacao(transacaoDoador);
            await _carteiraRepository.AdicionaTransacao(transacaoDoador);
            await _carteiraRepository.AtualizaCarteira(carteiradoador);
        }




        public async Task GerarCarteiraUsuario(int id)
        {
            Carteira carteira = new Carteira
            {
                UsuarioId = id
            };

            await _carteiraRepository.AdicionaCarteiraDoador(carteira);

        }
    }
}
