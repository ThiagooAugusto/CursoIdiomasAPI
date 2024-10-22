using CursoIdiomasAPI.Models;

namespace CursoIdiomasAPI.Repositories.Intefaces
{
    public interface IAlunoRepository
    {
        IEnumerable<Aluno> GetAll();
        Aluno? Get(Func<Aluno, bool> predicate);
        Aluno Add(Aluno aluno);
        Aluno Update(Aluno aluno);
        Aluno Delete(Aluno aluno);
    }
}
