using FoodREST.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FoodREST.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly currentAssembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(currentAssembly)
        );

        return services;
    }
}
