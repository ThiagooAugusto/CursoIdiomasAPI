using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Pagination;
using System.Linq.Expressions;


namespace CursoIdiomasAPI.Repositories.Intefaces
{
    public interface ITurmaRepository
    {
        Task<IEnumerable<Turma>> GetAllAsync();
        Task<Turma?> GetAsync(Expression<Func<Turma, bool>> predicate);
        Task<PagedList<Turma>> GetTurmasPagesAsync(TurmasParameters turmasParams);
        Task<PagedList<Turma>> GetTurmasFiltroNumeroAlunosAsync(TurmasFiltroNumeroAlunos turmasParams);
        Turma Add(Turma turma);
        Turma Delete(Turma turma);
    }
}
