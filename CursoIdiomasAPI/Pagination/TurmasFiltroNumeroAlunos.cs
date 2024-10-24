namespace CursoIdiomasAPI.Pagination
{
    public class TurmasFiltroNumeroAlunos:QueryStringParameters
    {
        public int? QuantidadeAlunos {  get; set; }
        public string? CriteriosFiltro {  get; set; }
    }
}
