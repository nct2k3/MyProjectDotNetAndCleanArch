using BookingFoodService.Common.Error;
using BookingFoodService.Common.FillterError;
using BookingFoodService.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BookingFoodService;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)

    {
        services.AddControllers(op=> op.Filters.Add<FillterErrorHandling>());
        services.AddSingleton<ProblemDetailsFactory, MyProblemDetailFactory>();
        services.AddMapping();
        return services;
    }
}