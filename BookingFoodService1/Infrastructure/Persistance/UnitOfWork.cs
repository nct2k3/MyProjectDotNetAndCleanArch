using Application.Common.Interfaces.Persistance;
using Infrastructure.DbContext;


namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var entityType = typeof(TEntity);
        if (!_repositories.ContainsKey(entityType))
        {
            var repositoryInstance = new Repository<TEntity>(_dbContext);
            _repositories[entityType] = repositoryInstance;
        }

        return (IRepository<TEntity>)_repositories[entityType]!;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}