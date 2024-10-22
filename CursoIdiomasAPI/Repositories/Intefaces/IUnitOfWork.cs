namespace CursoIdiomasAPI.Repositories.Intefaces
{
    public interface IUnitOfWork
    {
        ITurmaRepository TurmaRepository { get; }
        IAlunoRepository AlunoRepository { get; }
        void Commit();
        void Dispose();
    }
}
