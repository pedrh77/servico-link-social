namespace LinkSocial_Domain.Interfaces
{
    public interface IMd5HashService
    {
        Task<string> GerarHashMd5(string senha);
    }
}
