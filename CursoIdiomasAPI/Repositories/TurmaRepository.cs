using CursoIdiomasAPI.Context;
using CursoIdiomasAPI.Models;
using CursoIdiomasAPI.Pagination;
using CursoIdiomasAPI.Repositories.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomasAPI.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly AppDbContext _context;

        public TurmaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Turma Get(Func<Turma, bool> predicate)
        {
            return _context.Turmas.Include(t => t.Alunos).FirstOrDefault(predicate);
        }

        public IEnumerable<Turma> GetAll()
        {
            return _context.Turmas.Include(t=>t.Alunos).AsNoTracking();
        }

        public PagedList<Turma> GetTurmasPages(TurmasParameters turmasParams)
        {

            var turmas = GetAll().OrderBy(p => p.TurmaId).AsQueryable();

            var turmasOrdenadas = PagedList<Turma>.ToPagedList(turmas,
                       turmasParams.PageNumber, turmasParams.PageSize);

            return turmasOrdenadas;
        }
        public PagedList<Turma> GetTurmasFiltroNumeroAlunos(TurmasFiltroNumeroAlunos turmasParams)
        {
            var turmas = GetAll().AsQueryable();

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
            var turmasFiltradas = PagedList<Turma>.ToPagedList(turmas, turmasParams.PageNumber, turmasParams.PageSize);

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
