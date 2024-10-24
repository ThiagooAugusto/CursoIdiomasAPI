using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Pagination;

namespace CursoIdiomasAPI.Repositories.Intefaces
{
    public interface ITurmaRepository
    {
        IEnumerable<Turma> GetAll();
        Turma? Get(Func<Turma, bool> predicate);
        PagedList<Turma> GetTurmasPages(TurmasParameters turmasParams);
        PagedList<Turma> GetTurmasFiltroNumeroAlunos(TurmasFiltroNumeroAlunos turmasParams);
        Turma Add(Turma turma);
        Turma Delete(Turma turma);
    }
}
