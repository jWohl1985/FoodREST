using FoodREST.Application.Interfaces;
using FoodREST.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FoodREST.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IFoodRepository, InMemoryFoodRepository>();
        services.AddSingleton<IUnitOfWork, FakeUnitOfWork>();

        return services;
    }
}
