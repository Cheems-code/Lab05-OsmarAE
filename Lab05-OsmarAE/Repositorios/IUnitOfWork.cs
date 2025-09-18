namespace Lab05_OsmarAE.Repositorios;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> CompleteAsync();
}