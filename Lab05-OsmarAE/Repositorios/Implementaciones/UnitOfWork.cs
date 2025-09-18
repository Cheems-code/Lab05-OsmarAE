using System.Collections;
using Lab05_OsmarAE.Models;

namespace Lab05_OsmarAE.Repositorios.Implementaciones;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lab5Context _context;
    
    private Hashtable _repositorios;
    
    public UnitOfWork(Lab5Context context)
    {
        _context = context;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    
    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        if (_repositorios == null)
        {
            _repositorios = new Hashtable();
        }
        var type = typeof(TEntity).Name;

        if (!_repositorios.ContainsKey(type))
        {
            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositorios.Add(type, repositoryInstance);
        }
        return (IRepository<TEntity>)_repositorios[type]!;
    }
}