using System.Reflection;
using Application.Application.Commands.Register;
using Application.Application.Common;
using Application.Common.Behaviors;
using Application.Common.Interfaces.RabbitMQMessageQueeu;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommandHandler).Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<MessageQueeu>();
        services.AddHostedService<RabbitMQBackgroundConsumer>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

    
}