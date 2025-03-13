namespace FoodBillingService.Service.Common;

public interface IUnitOfWork
{
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    
}