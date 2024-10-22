using System.ComponentModel.DataAnnotations;

namespace CursoIdiomasAPI.Models.DTOs
{
    public class TurmaCreateDTO
    {
        [Required(ErrorMessage = "Campo Codigo é obrigatório!")]
        [StringLength(20, ErrorMessage = "Codigo deve ter no minimo 3 e maximo 20 caracteres!", MinimumLength = 3)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Campo Nivel é obrigatório!")]
        [Range(0, 2, ErrorMessage = "Nivel deve ser 0(iniciante), 1(intermediário) ou 2(avançado) !")]
        public Nivel Nivel { get; set; }
    }
}
