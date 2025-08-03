using LinkSocial_Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace LinkSocial_Domain.DTO.Request
{
    public class NovaDoacaoRequestDTO
    {
        [Required(ErrorMessage = "O ID do doador é obrigatório.")]
        public int DoadorId { get; set; }

        [Required(ErrorMessage = "O ID da ONG é obrigatório.")]
        public int OngId { get; set; }

        [Required(ErrorMessage = "O ID do benefício é obrigatório.")]
        public int BeneficioId { get; set; }

        [Required(ErrorMessage = "O valor da doação é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O tipo de doação é obrigatório.")]
        [EnumDataType(typeof(TipoDoacao))]
        public TipoDoacao TipoDoacao { get; set; }

        public string? Comentario { get; set; }
    }
} 