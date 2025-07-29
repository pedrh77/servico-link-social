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
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Beneficio> Beneficos { get; set; }
    }
}
