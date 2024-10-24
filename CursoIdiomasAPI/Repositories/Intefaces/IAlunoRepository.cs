using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Pagination;

namespace CursoIdiomasAPI.Repositories.Intefaces
{
    public interface IAlunoRepository
    {
        //IEnumerable<Aluno> GetAlunosPages(AlunosParameters alunosParams);
        PagedList<Aluno> GetAlunosPages(AlunosParameters alunosParams);
        PagedList<Aluno> GetAlunosNomeFiltro(AlunosFiltroNome alunosParams);
        IEnumerable<Aluno> GetAll();
        Aluno? Get(Func<Aluno, bool> predicate);
        Aluno Add(Aluno aluno);
        Aluno Update(Aluno aluno);
        Aluno Delete(Aluno aluno);
    }
}
