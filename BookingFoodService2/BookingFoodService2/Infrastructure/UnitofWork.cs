using System;
using System.Collections.Generic;
using BookingFoodService2.Controller;
using BookingFoodServie2.Controller;
using MongoDB.Driver;

namespace BookingFoodService2.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMongoDatabase _database;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(IMongoDatabase database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
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