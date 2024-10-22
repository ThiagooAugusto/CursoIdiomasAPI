using CursoIdiomasAPI.Context;
using CursoIdiomasAPI.Models;
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
            return _context.Turmas.AsNoTracking();
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
