
using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Interfaces.Carteiras
{
    public interface ICarteiraService
    {
        
        Task AdicionarTransacao(int doadorid, int? ongid, TipoTransacao tipo, decimal valor);
        Task GerarCarteiraUsuario(int id);
    }
}
