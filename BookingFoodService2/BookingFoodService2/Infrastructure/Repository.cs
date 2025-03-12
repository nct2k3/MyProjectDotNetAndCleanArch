using System.Linq.Expressions;
using BookingFoodService2.Controller;
using MongoDB.Driver;

namespace BookingFoodService2.Infrastructure;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly IMongoCollection<TEntity> _collection;

    public Repository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<TEntity>(collectionName);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Guid id, TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<TEntity>> AllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _collection.Find(predicate).SingleOrDefaultAsync();
    }
}