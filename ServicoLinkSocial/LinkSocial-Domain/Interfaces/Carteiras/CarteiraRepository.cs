using LinkSocial_Domain.Enum;
using LinkSocial_Domain.Models;

namespace LinkSocial_Domain.Interfaces.Carteiras
{
    public interface ICarteiraRepository
    {
        Task AdicionaCarteiraDoador( Carteira carteira);
        Task AdicionaTransacao(Transacao transacaoDoador);
        Task AtualizaCarteira(Carteira carteiradoador);
        Task<Carteira> BuscaCarteiraDoador(int doadorid);
    }
}
