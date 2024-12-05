using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FoodREST.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly currentAssembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(currentAssembly)
        );

        return services;
    }
}
