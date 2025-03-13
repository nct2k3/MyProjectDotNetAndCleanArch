using FoodBillingService.Infrastructure.Database;
using FoodBillingService.Service.Common;
using MongoDB.Driver;

namespace FoodBillingService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbContext>();
        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var context = sp.GetRequiredService<MongoDbContext>();
            return context.Database;
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfwork>();
        

        return services;
    }
}