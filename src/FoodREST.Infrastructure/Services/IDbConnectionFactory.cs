using Npgsql;
using System.Data;

namespace FoodREST.Infrastructure.Services;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
}

internal sealed class NpgSqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public NpgSqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}