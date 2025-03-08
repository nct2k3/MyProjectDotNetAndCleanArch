using System.Reflection;
using Application.Authentication.Comands.Register;
using Application.Authentication.Commands.Register;
using Application.Common.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Authentication.Comands.Register;
using Presentation.Common.Behaviors;

namespace Presentation;

public static class DependencyInjection
{
    
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Đăng ký MediatR và handler từ assembly chứa RegisterCommandHandler
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommandHandler).Assembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    
    
}