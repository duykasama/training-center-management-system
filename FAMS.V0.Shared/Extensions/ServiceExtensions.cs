using System.Reflection;
using System.Text;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Interfaces;
using FAMS.V0.Shared.Repositories;
using FAMS.V0.Shared.Settings;
using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;


namespace FAMS.V0.Shared.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
       Console.WriteLine("can be here");
        services.AddSingleton(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            return new MongoClient(configuration.GetConnectionString(DbConnection.MongoDbConnection)).GetDatabase(
                Database.FAMS_DB);
        });

        return services;
    }
    
    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(serviceProvider =>
        {
            var database = serviceProvider.GetService<IMongoDatabase>();
            return new MongoRepository<T>(database, collectionName);
        });

        return services;
    }

    public static IServiceCollection AddRabbitMq(this IServiceCollection services, string serviceName)
    {
        services.AddMassTransit(configure =>
        {
            configure.AddConsumers(Assembly.GetEntryAssembly());
            
            configure.UsingRabbitMq((ctx, configurator) =>
            {
                var configuration = ctx.GetService<IConfiguration>();
                var rabbitMqHost = configuration.GetSection(nameof(RabbitMqSettings)).GetSection(nameof(RabbitMqSettings.Host));
                configurator.Host(rabbitMqHost.Value);
                configurator.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter(serviceName, false));
            });
        });

        services.AddMassTransitHostedService();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(builder =>
        {

            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            
            builder.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey))
            };
        });

        return services;
    }

    public static IServiceCollection AddCorsDefault(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("fams-cors-policy", builder =>
            {
                builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });
        });
        
        return services;
    }
}