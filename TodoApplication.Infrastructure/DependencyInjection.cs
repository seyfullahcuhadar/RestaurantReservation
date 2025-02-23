using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TodoApplication.Application.Abstractions.Authentication;
using TodoApplication.Application.Abstractions.Clock;
using TodoApplication.Domain.Abstractions;
using TodoApplication.Domain.Todo;
using TodoApplication.Infrastructure.Authentication;
using TodoApplication.Infrastructure.Authentication.Models;
using TodoApplication.Infrastructure.Clock;
using TodoApplication.Infrastructure.Repositories;


namespace TodoApplication.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        AddPersistence(services, configuration);
        AddOpenTelemetry(services);
        return services;
    }
    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
        
        AddAuthentication(services, configuration);
        services.AddScoped<ITodoRepository, TodoRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        

        
        
    }
    private static void AddOpenTelemetry(IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("TodoApp"))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddJaegerExporter()
                .AddConsoleExporter())
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter()
                .AddPrometheusExporter());
    }
    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IdentityTokenClaimService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var authenticationOptions = configuration.GetSection("Authentication").Get<AuthenticationOptions>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions?.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }
}

// OpenTelemetry.Exporter.Jaeger paketini eklemek için terminalde şunu çalıştırın:
// dotnet add package OpenTelemetry.Exporter.Jaeger
