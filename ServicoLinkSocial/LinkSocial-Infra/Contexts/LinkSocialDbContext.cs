using LinkSocial_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkSocial_Infra.Contexts
{
    public class LinkSocialDbContext : DbContext
    {
        public LinkSocialDbContext(DbContextOptions<LinkSocialDbContext> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Beneficio> Beneficios { get; set; }
        public DbSet<Doacao> Doacoes { get; set; }

        public DbSet<Carteira> Carteiras { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
    }
}
