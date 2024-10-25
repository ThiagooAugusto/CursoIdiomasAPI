using CursoIdiomasAPI.Context;
using CursoIdiomasAPI.Repositories.Intefaces;

namespace CursoIdiomasAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private  IAlunoRepository _alunoRepository;
        private  ITurmaRepository _turmaRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ITurmaRepository TurmaRepository {
            get {
                return _turmaRepository = _turmaRepository ?? new TurmaRepository(_context);
            }
        }

        public IAlunoRepository AlunoRepository
        {
            get
            {
                return _alunoRepository = _alunoRepository ?? new AlunoRepository(_context);
            }
        }

        public async Task CommitAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
