using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Infra.Contexts;

namespace LinkSocial_Infra.Repository
{
    public class UsuarioRepository(LinkSocialDbContext _context) : IUsuarioRepository
    {
    }
}
