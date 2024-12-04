using FoodREST.Application.Interfaces;
using FoodREST.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FoodREST.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFoodRepository, InMemoryFoodRepository>();
        services.AddScoped<IUnitOfWork, FakeUnitOfWork>();

        return services;
    }
}
