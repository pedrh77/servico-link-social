using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkSocial_Domain.DTO.Request
{
    public class AtualizaDadosUsuarioRequestDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O nome deve ter no máximo 255 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; }

        public string? Comentario { get; set; } = null;
    }
}
