namespace LinkSocial_Domain.Models
{
    public class EntityBase
    {
        public DateTime Criado_em { get; set; }
        public DateTime Modificado_em { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; } = false;
    }
}
