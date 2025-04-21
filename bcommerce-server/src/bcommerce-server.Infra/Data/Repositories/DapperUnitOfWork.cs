using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace bcommerce_server.Infra.Repositories;

public class DapperUnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly NpgsqlConnection _connection;
    private NpgsqlTransaction? _transaction;
    private bool _disposed;

    public DapperUnitOfWork(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        _connection = new NpgsqlConnection(connectionString);
    }

    // ✅ Corrigido para interface genérica
    public IDbConnection Connection => _connection;

    public IDbTransaction Transaction =>
        _transaction ?? throw new InvalidOperationException("Transação não foi iniciada. Chame Begin().");

    public async Task Begin()
    {
        if (_connection.State != ConnectionState.Open)
            await _connection.OpenAsync();

        _transaction = await _connection.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Não é possível fazer commit sem uma transação ativa.");

        await _transaction.CommitAsync();
        await CleanupAsync();
    }

    public async Task Rollback()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Não é possível fazer rollback sem uma transação ativa.");

        await _transaction.RollbackAsync();
        await CleanupAsync();
    }

    private async Task CleanupAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        if (_connection.State == ConnectionState.Open)
        {
            await _connection.CloseAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        if (_connection.State == ConnectionState.Open)
        {
            await _connection.CloseAsync();
        }

        await _connection.DisposeAsync();
        _disposed = true;
    }
}
