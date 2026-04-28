using DiceBound.Common;
using DiceBound.Interfaces;
using DiceBound.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class GenericRepository<T> : IGenericRepository<T>
    where T : BaseEntity
{
    protected readonly DiceBoundDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DiceBoundDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
        => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);

    public void Update(T entity)
        => _dbSet.Update(entity);

    public void Delete(T entity)
        => _dbSet.Remove(entity);

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
}