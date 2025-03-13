using Application.Common.Interfaces.Service;
using Infrastructure.Common.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.RabbitMQMessageQueeu;
using Infrastructure.Common.Authentication;
using Infrastructure.Common.Dbcontext;
using Infrastructure.Common.RabbitMQ;
using Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;


namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this  IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRabbitMqMessageQueeu, RabbitMqMessageQueeu>();
        services.AddSingleton<IRabbitMQComsumer, RabbitMQComsumer>();
        

        return services;
        
    }
    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        // Đăng ký JwtSettings từ appsettings.json
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        // Đăng ký JwtTokenGenerator
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        

        // Đăng ký Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "NCT",
                    ValidAudience = "NCT",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("a-string-secret-at-least-256-bits-long"))
                };



            });
        services.AddAuthorization(options =>
        {
            // Policy chỉ cho Admin
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin")); 

            // Policy chỉ cho User
            options.AddPolicy("UserOnly", policy =>
                policy.RequireRole("User")); 

            // Policy cho cả Admin và User
            options.AddPolicy("AdminOrUser", policy =>
                policy.RequireRole("Admin", "User")); 
        });



        return services;
    }
    
    
    
}