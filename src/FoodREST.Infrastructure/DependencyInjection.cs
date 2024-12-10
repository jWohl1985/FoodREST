using FoodREST.Application.Interfaces;
using FoodREST.Infrastructure.Persistence;
using FoodREST.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodREST.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
    {
        string connectionString = config.GetConnectionString("NpgSqlConnectionString")!;

        services.AddScoped<IFoodRepository, EFCoreFoodRepository>();
        services.AddDbContext<FoodContext>(options => options.UseNpgsql(connectionString));

        return services;
    }
}
