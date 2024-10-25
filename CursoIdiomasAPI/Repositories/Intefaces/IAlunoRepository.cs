using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Pagination;
using System.Linq.Expressions;


namespace CursoIdiomasAPI.Repositories.Intefaces
{
    public interface IAlunoRepository
    {
        //IEnumerable<Aluno> GetAlunosPages(AlunosParameters alunosParams);
        Task<PagedList<Aluno>> GetAlunosPagesAsync(AlunosParameters alunosParams);
        Task<PagedList<Aluno>> GetAlunosNomeFiltroAsync(AlunosFiltroNome alunosParams);
        Task<IEnumerable<Aluno>> GetAllAsync();
        Task<Aluno?> GetAsync(Expression<Func<Aluno, bool>> predicate);
        Aluno Add(Aluno aluno);
        Aluno Update(Aluno aluno);
        Aluno Delete(Aluno aluno);
    }
}
