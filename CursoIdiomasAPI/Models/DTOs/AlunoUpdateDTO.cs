using CursoIdiomasAPI.Validation;
using System.ComponentModel.DataAnnotations;

namespace CursoIdiomasAPI.Models.DTOs
{
    public class AlunoUpdateDTO
    {
        [Required(ErrorMessage ="Campo AlunoId é obrigatório!")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório!")]
        [StringLength(50, ErrorMessage = "Nome deve ter no maximo 50 caracteres!")]
        [NaoContemNumero]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo CPF é obrigatório!")]
        [CPFValido]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo Matricula é obrigatório!")]
        [StringLength(11, ErrorMessage = "Matricula deve ter no maximo 11 caracteres")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "Campo Email é obrigatório!")]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "Email deve ter no maximo 50 caracteres")]
        public string Email { get; set; }
    }
}
