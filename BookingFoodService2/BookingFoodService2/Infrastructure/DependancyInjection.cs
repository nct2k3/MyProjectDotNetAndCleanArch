using BookingFoodService2.Controller;
using BookingFoodServie2.Controller;
using BookingFoodServie2.Data;
using MongoDB.Driver;

namespace BookingFoodService2.Infrastructure;


    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)

        {
           
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<ApplicationDbContext>();
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var context = sp.GetRequiredService<ApplicationDbContext>();
                return context.Database;
            });
            
            return services;
        }
    
    }
