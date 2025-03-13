namespace FoodBillingService.Service.Common;

public interface IRepository<TEntity> where TEntity : class
{
    Task SaveOrderAsync(TEntity entity);
    Task<TEntity> GetOrderByIdAsync(Guid orderId);
    Task<List<TEntity>> GetAllOrdersAsync();
}