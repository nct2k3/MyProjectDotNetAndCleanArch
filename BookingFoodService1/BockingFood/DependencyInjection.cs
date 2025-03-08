using BockingFood.Common.Errors;
using BockingFood.Common.Mapping;
using BockingFood.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BockingFood;

public static class DependancyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)

    {
        services.AddControllers(options=>options.Filters.Add<ErrorHandlingFilterAttribute>()
                                );
        services.AddMapping();
        services.AddSingleton<ProblemDetailsFactory, MyProblemDetailFactory>();
        
        return services;
    }
}