using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.Application.Abstractions.Authentication;
using RestaurantReservation.Application.Abstractions.Clock;
using RestaurantReservation.Domain.Abstractions;
using RestaurantReservation.Infrastructure.Authentication;
using RestaurantReservation.Infrastructure.Authentication.Models;
using RestaurantReservation.Infrastructure.Clock;

namespace RestaurantReservation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        AddPersistence(services, configuration);

        return services;
    }
    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
        
        AddAuthentication(services, configuration);

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        
        //services.AddSingleton<ISqlConnectionFactory>(_ =>
        //    new SqlConnectionFactory(connectionString));

        //SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
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