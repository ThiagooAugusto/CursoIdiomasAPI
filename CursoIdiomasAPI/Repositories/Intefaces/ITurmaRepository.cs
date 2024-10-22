using CursoIdiomasAPI.Models;

namespace CursoIdiomasAPI.Repositories.Intefaces
{
    public interface ITurmaRepository
    {
        IEnumerable<Turma> GetAll();
        Turma? Get(Func<Turma, bool> predicate);
        Turma Add(Turma turma);
        Turma Delete(Turma turma);
    }
}
