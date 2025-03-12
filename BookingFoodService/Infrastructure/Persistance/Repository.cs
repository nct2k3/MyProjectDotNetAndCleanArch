using System.Linq.Expressions;
using Application.Common.Interfaces.Persistance;
using Infrastructure.Common.Dbcontext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class Repository<TEntity>:IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    
    public async Task<TEntity> GetByIdAsync(Guid id)
    {
       return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task UpdateAsync(TEntity entity)
    {
       _dbSet.Update(entity);
       return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity)
    {
       _dbSet.Remove(entity);
       return Task.CompletedTask;
    }
    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        
        return await _dbSet.SingleOrDefaultAsync(predicate);
    }
}