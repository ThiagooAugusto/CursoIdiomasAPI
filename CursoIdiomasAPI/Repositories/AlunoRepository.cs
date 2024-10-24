using CursoIdiomasAPI.Context;
using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Pagination;
using CursoIdiomasAPI.Repositories.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomasAPI.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Aluno Get(Func<Aluno, bool> predicate)
        {
            return _context.Alunos.Include(a=>a.Turmas).FirstOrDefault(predicate);
        }

        public IEnumerable<Aluno> GetAll()
        {
            return _context.Alunos.AsNoTracking();
        }
        //public IEnumerable<Aluno> GetAlunosPages(AlunosParameters alunosParams)
        //{
        //    return GetAll()
        //        .OrderBy(p=>p.Nome)
        //        .Skip((alunosParams.PageNumber -1) * alunosParams.PageSize)
        //        .Take(alunosParams.PageSize).ToList();
        //}

        //Mudança da lógica de paginação para a classe PagedList<T>
        public PagedList<Aluno> GetAlunosPages(AlunosParameters alunosParameters)
        {
           
            var alunos = GetAll().OrderBy(p => p.AlunoId).AsQueryable();

            var alunosOrdenados = PagedList<Aluno>.ToPagedList(alunos,
                       alunosParameters.PageNumber, alunosParameters.PageSize);

            return alunosOrdenados;
        }

        public PagedList<Aluno> GetAlunosNomeFiltro(AlunosFiltroNome alunosParams)
        {
            var alunos = GetAll().AsQueryable();

            if(!string.IsNullOrEmpty(alunosParams.Nome))
                alunos = alunos.Where(a=>a.Nome.Contains(alunosParams.Nome));

            //paginação dos dados filtrados
            var alunosFiltrados = PagedList<Aluno>.ToPagedList(alunos, alunosParams.PageNumber, alunosParams.PageSize);

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
