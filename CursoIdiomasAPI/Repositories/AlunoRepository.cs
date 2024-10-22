using CursoIdiomasAPI.Context;
using CursoIdiomasAPI.Models;
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
