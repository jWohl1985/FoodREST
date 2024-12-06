using Dapper;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using System.Data;

namespace FoodREST.Infrastructure.Services;

public class DapperFoodRepository : IFoodRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DapperFoodRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> AddFoodAsync(Food food, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        CommandDefinition sql = new("""
            insert into foods (id, name, calories, proteingrams, carbohydrategrams, fatgrams)
            values (@Id, @Name, @Calories, @ProteinGrams, @CarbohydrateGrams, @FatGrams)
            """, food, cancellationToken: token);

        return await connection.ExecuteAsync(sql) > 0;
    }

    public async Task<Food?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        CommandDefinition sql = new("""
            select * from foods
            where id = @Id
            """, new { Id = id }, cancellationToken: token);

        var result = await connection.QuerySingleAsync<Food>(sql);

        return result;
    }

    public async Task<IEnumerable<Food>> GetAllAsync(CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        CommandDefinition sql = new("select * from foods", cancellationToken: token);
        var result = await connection.QueryAsync(sql);

        return result.Select(x => new Food(x.id, x.name, x.calories, x.proteingrams, x.carbohydrategrams, x.fatgrams));
    }

    public async Task<Food?> UpdateFoodAsync(Guid id, Food food, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        CommandDefinition sql = new("""
            update foods set name = @Name, calories = @Calories,
            proteinGrams = @ProteinGrams, carbohydrateGrams = @CarbohydrateGrams, fatGrams = @FatGrams
            where id = @Id
            """, new { 
                    Id = id, 
                    Name = food.Name, 
                    Calories = food.Calories, 
                    ProteinGrams = food.ProteinGrams, 
                    CarbohydrateGrams = food.CarbohydrateGrams, 
                    FatGrams = food.FatGrams }, cancellationToken: token);

        return await connection.ExecuteAsync(sql) > 0
            ? food
            : null;
    }

    public async Task<bool> DeleteFoodAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        CommandDefinition sql = new("delete from foods where id = @Id", new { Id = id }, cancellationToken: token);

        return await connection.ExecuteAsync(sql) > 0;
    }
}
