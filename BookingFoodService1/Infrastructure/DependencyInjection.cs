﻿using System.Text;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Service;
using Infrastructure.Authentiscation;
using Infrastructure.Persistence;
using Infrastructure.Service;

using Microsoft.Extensions.DependencyInjection;
using Presentation.Common.Interfaces.MessageQueueService;

namespace Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this  IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DataTimeProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
        
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        // Đăng ký JwtSettings từ appsettings.json
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        // Đăng ký JwtTokenGenerator
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddScoped<IMessageQueueService, RabbitMQService.RabbitMQService>();

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