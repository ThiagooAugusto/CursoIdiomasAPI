namespace CursoIdiomasAPI.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public string CPF {  get; set; }
        public string Email {  get; set; }
        public List<Turma> Turmas { get; set; } = [];

    }
}
