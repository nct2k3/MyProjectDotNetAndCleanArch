using FoodBillingService.Service.Common;
using MongoDB.Driver;

namespace FoodBillingService.Infrastructure;

public class UnitOfwork:IUnitOfWork
{
    private readonly IMongoDatabase _database;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfwork(IMongoDatabase database)
    {
        _database = database;
    }
    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var entityType = typeof(TEntity);

        if (!_repositories.ContainsKey(entityType))
        {
            // Sử dụng tên collection tương ứng với tên của TEntity
            var collectionName = entityType.Name;
            var repositoryInstance = new Repository<TEntity>( _database, collectionName);
            _repositories[entityType] = repositoryInstance;
        }

        return (IRepository<TEntity>)_repositories[entityType]!;
    }


    
}