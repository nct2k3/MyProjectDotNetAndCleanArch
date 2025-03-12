using BookingFoodService2.Controller;
using BookingFoodService2.Infrastructure;
using BookingFoodServie2.Controller;
using BookingFoodServie2.Service.Comands;
using BookingFoodServie2.Service.Mapping;

namespace BookingFoodServie2.Service;

public static class DependancyInjection
{
    public static IServiceCollection AddService(this IServiceCollection services)

    {
        services.AddAutoMapper(typeof(Mealmaping));
        services.AddScoped<MealCommand>();
        return services;
    }
    
}