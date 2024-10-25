using CursoIdiomasAPI.Context;
using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Pagination;
using CursoIdiomasAPI.Repositories.Intefaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CursoIdiomasAPI.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Aluno> GetAsync(Expression<Func<Aluno, bool>> predicate)
        {
            return await _context.Alunos.Include(a=>a.Turmas).FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Aluno>> GetAllAsync()
        {
            return await  _context.Alunos.AsNoTracking().ToListAsync();
        }
        //public IEnumerable<Aluno> GetAlunosPages(AlunosParameters alunosParams)
        //{
        //    return GetAll()
        //        .OrderBy(p=>p.Nome)
        //        .Skip((alunosParams.PageNumber -1) * alunosParams.PageSize)
        //        .Take(alunosParams.PageSize).ToList();
        //}

        //Mudança da lógica de paginação para a classe PagedList<T>
        public async Task<PagedList<Aluno>> GetAlunosPagesAsync(AlunosParameters alunosParameters)
        {

            var alunos = await GetAllAsync();

            var alunosOrdenados = alunos.OrderBy(p => p.AlunoId).AsQueryable();

            var resultado = PagedList<Aluno>.ToPagedList(alunosOrdenados,
                       alunosParameters.PageNumber, alunosParameters.PageSize);

            return resultado;
        }

        public async Task<PagedList<Aluno>> GetAlunosNomeFiltroAsync(AlunosFiltroNome alunosParams)
        {
            var alunos = await GetAllAsync();

            if(!string.IsNullOrEmpty(alunosParams.Nome))
                alunos = alunos.Where(a=>a.Nome.Contains(alunosParams.Nome));

            //paginação dos dados filtrados
            var alunosFiltrados = PagedList<Aluno>.ToPagedList(alunos.AsQueryable(), alunosParams.PageNumber, alunosParams.PageSize);

            return alunosFiltrados;
        }
        public Aluno Add(Aluno aluno)
        {
            _context.Add(aluno);
            return aluno;
        }

        public Aluno Delete(Aluno aluno)
        {
            _context.Remove(aluno);
            return aluno;
        }

      
        public Aluno Update(Aluno aluno)
        {
            _context.Update(aluno);
            return aluno;
        }

       
    }
}
