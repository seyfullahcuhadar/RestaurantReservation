using Microsoft.Extensions.DependencyInjection;
using TodoApplication.Application.Behaviors;

namespace TodoApplication.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

            //configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

            //configuration.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });


        //services.AddTransient<PricingService>();

        return services;
    }
}