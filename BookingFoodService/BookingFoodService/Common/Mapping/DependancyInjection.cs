using Application.Common.Interfaces.Authentication;

namespace BookingFoodService.Common.Mapping;
using System.Reflection;
using Mapster;
using MapsterMapper;
public static class DependancyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config =TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetCallingAssembly());
        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();
        return services;
    }
    
}