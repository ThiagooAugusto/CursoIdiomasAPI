using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CursoIdiomasAPI.Models
{
    [Table("Alunos")]
    public class Aluno
    {
        [Key] 
        [Required]
        public int AlunoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        public string CPF {  get; set; }

        [Required]
        [StringLength(11)]
        public string Matricula { get; set; }

        [Required]
        [StringLength(50)]
        public string Email {  get; set; }

        [JsonIgnore]
        public List<Turma> Turmas { get; set; } = [];

    }
}
