namespace LinkSocial_Domain.DTO.Response
{
    public class CarteiraResponseDTO
    {
        public int Id { get; set; }
        public decimal Saldo { get; set; }
        public string Status { get; set; }
        public List<TransacaoDto> Transacoes { get; set; } = new();
    }

    public class TransacaoDto
    {
        public int Id { get; set; }
        public string NomeTransacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
    }

}
