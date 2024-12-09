using Dapper;

namespace FoodREST.Infrastructure.Services;

public sealed class DbInitializer
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DbInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        CommandDefinition createTable = new("""
            create table if not exists foods (
            id UUID primary key,
            name TEXT not null,
            calories int not null,
            proteinGrams int not null,
            carbohydrateGrams int not null,
            fatGrams int);
            """);

        await connection.ExecuteAsync(createTable);
    }
}
