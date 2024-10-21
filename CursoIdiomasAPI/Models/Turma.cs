using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoIdiomasAPI.Models
{
    [Table("Turmas")]
    public class Turma
    {
        [Key]
        [Required]
        public int TurmaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Codigo {  get; set; }

        [Required]
        public Nivel Nivel { get; set; }
        public List<Aluno> Alunos { get; set; } = [];

    }
    public enum Nivel
    {
        Iniciante = 0,
        Intermediario = 1,
        Avançado = 2
    }
}
