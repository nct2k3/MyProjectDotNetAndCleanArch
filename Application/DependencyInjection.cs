using System.Reflection;
using Application.Authentication.Comands.Register;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation;

public static class DependencyInjection
{
    
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Đăng ký MediatR và handler từ assembly chứa RegisterCommandHandler
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommandHandler).Assembly));

            return services;
        }
    
    
}