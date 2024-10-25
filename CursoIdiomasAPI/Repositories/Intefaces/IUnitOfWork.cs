namespace CursoIdiomasAPI.Repositories.Intefaces
{
    public interface IUnitOfWork
    {
        ITurmaRepository TurmaRepository { get; }
        IAlunoRepository AlunoRepository { get; }
        Task CommitAsync();
        void Dispose();
    }
}
