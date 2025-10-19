using System.Linq.Expressions;

namespace BestPracticesJWT.Core.Interfaces.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddSync(T entity);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);
    void Remove(T entity);
    T Update(T entity);
}
