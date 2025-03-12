using System.Linq.Expressions;

namespace BookingFoodService2.Controller;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);

    Task UpdateAsync(Guid id, TEntity entity);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<TEntity>> AllAsync();

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
}