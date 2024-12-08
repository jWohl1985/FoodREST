using FoodREST.Application.Interfaces;
using System.Data;

namespace FoodREST.Infrastructure.Services;

public class DapperUnitOfWork : IUnitOfWork
{
    private readonly IDbConnectionFactory _connectionFactory;

    private IDbConnection? _currentConnection;
    private IDbTransaction? _currentTransaction;

    public DapperUnitOfWork(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public bool HasStarted { get; private set; }

    public async Task BeginAsync(CancellationToken token = default)
    {
        if (!HasStarted)
        {
            HasStarted = true;
            _currentConnection = await _connectionFactory.CreateConnectionAsync(token);
            _currentTransaction = _currentConnection.BeginTransaction();
        }
    }

    public void Rollback()
    {
        if (_currentTransaction is not null && _currentConnection is not null)
        {
            _currentTransaction.Rollback();
            _currentTransaction.Dispose();

            _currentConnection.Dispose();

            _currentConnection = null;
            _currentTransaction = null;
            HasStarted = false;
        }
    }

    public void SaveChanges()
    {
        if (_currentTransaction is not null && _currentConnection is not null)
        {
            _currentTransaction.Commit();
            _currentTransaction.Dispose();

            _currentConnection.Dispose();

            _currentTransaction = null;
            _currentConnection = null;
            HasStarted = false;
        }
    }
}
