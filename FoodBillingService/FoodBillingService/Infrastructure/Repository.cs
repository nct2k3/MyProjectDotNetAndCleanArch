using FoodBillingService.Service.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodBillingService.Infrastructure;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly IMongoCollection<TEntity> _collection;

    public Repository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<TEntity>(collectionName);
    }

    public async Task SaveOrderAsync(TEntity entity)
    {
        try
        {
            await _collection.InsertOneAsync(entity);
            Console.WriteLine("Order has been successfully saved.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save order: {ex.Message}");
        }
    }


    public async Task<TEntity> GetOrderByIdAsync(Guid orderId)
    {
        var filter = Builders<TEntity>.Filter.Eq("OrderId", orderId);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> GetAllOrdersAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}