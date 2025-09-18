using System.Linq.Expressions;

namespace Lab05_OsmarAE.Repositorios;

public interface IRepository<T> where T : class
{
    
    // Obtener una entidad por su ID
    Task<T?> GetByIdAsync(int id);

    // Obtener todas las entidades
    Task<IEnumerable<T>> GetAllAsync();

    // Obtener entidades que cumplen una condición
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

    // Añadir una nueva entidad
    Task AddAsync(T entity);

    // Añadir un rango de entidades
    Task AddRangeAsync(IEnumerable<T> entities);
    
    // Eliminar una entidad
    void Remove(T entity);

    // Eliminar un rango de entidades
    void RemoveRange(IEnumerable<T> entities);
    
    // Marca una entidad como modificada.
    void Update(T entity);
    
}