namespace CursoIdiomasAPI.Models
{
    public class Turma
    {
        public int TurmaId { get; set; }
        public string Codigo {  get; set; }
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
