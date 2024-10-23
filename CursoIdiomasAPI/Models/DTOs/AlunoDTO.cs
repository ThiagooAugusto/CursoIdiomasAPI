using System.ComponentModel.DataAnnotations;

namespace CursoIdiomasAPI.Models.DTOs
{
    public class AlunoDTO
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Matricula { get; set; }
        public string Email { get; set; }

        public List<Turma> Turmas { get; set; } = [];

    }
}
