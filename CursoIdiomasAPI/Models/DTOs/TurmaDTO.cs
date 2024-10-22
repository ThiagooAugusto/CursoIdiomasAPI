using System.ComponentModel.DataAnnotations;

namespace CursoIdiomasAPI.Models.DTOs
{
    public class TurmaDTO
    {
        public int TurmaId { get; set; }
        public string Codigo { get; set; }
        public Nivel Nivel { get; set; }
        public List<Aluno> Alunos { get; set; } = [];
    }
}
