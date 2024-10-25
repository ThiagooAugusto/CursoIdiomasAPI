using CursoIdiomasAPI.Context;
using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Pagination;
using CursoIdiomasAPI.Repositories.Intefaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CursoIdiomasAPI.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly AppDbContext _context;

        public TurmaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Turma> GetAsync(Expression <Func <Turma, bool>> predicate)
        {
            return await _context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(predicate);
        }

        public async Task <IEnumerable<Turma>> GetAllAsync()
        {
            return await _context.Turmas.Include(t=>t.Alunos).AsNoTracking().ToListAsync();
        }

        public async Task<PagedList<Turma>> GetTurmasPagesAsync(TurmasParameters turmasParams)
        {

            var turmas = await GetAllAsync();

            var turmasOrdenadas = turmas.OrderBy(p => p.TurmaId).AsQueryable();

            var resultado = PagedList<Turma>.ToPagedList(turmasOrdenadas,
                       turmasParams.PageNumber, turmasParams.PageSize);

            return resultado;
        }
        public async Task<PagedList<Turma>> GetTurmasFiltroNumeroAlunosAsync(TurmasFiltroNumeroAlunos turmasParams)
        {
            var turmas = await GetAllAsync();

            if (!string.IsNullOrEmpty(turmasParams.CriteriosFiltro) && turmasParams.QuantidadeAlunos.HasValue)
            {
                if (turmasParams.CriteriosFiltro.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    turmas = turmas.Where(t=>t.Alunos.Count > turmasParams.QuantidadeAlunos);
                }
                else if (turmasParams.CriteriosFiltro.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    turmas = turmas.Where(t => t.Alunos.Count < turmasParams.QuantidadeAlunos);
                }
                else if (turmasParams.CriteriosFiltro.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    turmas = turmas.Where(t => t.Alunos.Count == turmasParams.QuantidadeAlunos);
                }
            }

            //paginação dos dados filtrados
            var turmasFiltradas = PagedList<Turma>.ToPagedList(turmas.AsQueryable(), turmasParams.PageNumber, turmasParams.PageSize);

            return turmasFiltradas;
        }
        public Turma Add(Turma turma)
        {
           _context.Turmas.Add(turma);
            return turma;
        }

        public Turma Delete(Turma turma)
        {
            _context.Turmas.Remove(turma);
            return turma;
        }

        
    }
}
