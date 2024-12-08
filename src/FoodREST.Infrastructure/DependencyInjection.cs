using FoodREST.Application.Interfaces;
using FoodREST.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodREST.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
    {
        string connectionString = config.GetConnectionString("NpgSqlConnectionString")!;

        services.AddSingleton<IDbConnectionFactory>(_ => new NpgSqlConnectionFactory(connectionString));
        services.AddSingleton<IFoodRepository, DapperFoodRepository>();
        services.AddSingleton<IUnitOfWork, DapperUnitOfWork>();
        services.AddSingleton<DbInitializer>();

        return services;
    }
}
