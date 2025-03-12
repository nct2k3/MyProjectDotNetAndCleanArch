using BookingFoodService2.Controller;

namespace BookingFoodServie2.Controller;

public interface IUnitOfWork
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    
}