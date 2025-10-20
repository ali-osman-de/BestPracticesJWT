using BestPracticesJWT.Core.Interfaces.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BestPracticesJWT.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context, DbSet<T> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public async Task AddSync(T entity) 
        => await _dbSet.AddAsync(entity);

    public async Task<IEnumerable<T>> GetAllAsync() 
        => await _dbSet.ToListAsync();


    public async Task<T> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if(entity!=null) _context.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression) 
        => _dbSet.Where(expression);


    public void Remove(T entity) 
        => _dbSet.Remove(entity);
    

    public T Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}
