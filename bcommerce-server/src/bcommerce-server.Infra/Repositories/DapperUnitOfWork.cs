using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace bcommerce_server.Infra.Repositories
{


    public class DapperUnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly NpgsqlConnection _connection;
        private NpgsqlTransaction? _transaction;

        public DapperUnitOfWork(IConfiguration configuration)
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public NpgsqlConnection Connection => _connection;

        public NpgsqlTransaction Transaction => _transaction
                                                ?? throw new InvalidOperationException("Transaction has not been started.");

        public async Task Begin()
        {
            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            _transaction = await _connection.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            if (_transaction is null) return;
            await _transaction.CommitAsync();
            await _connection.CloseAsync();
        }

        public async Task Rollback()
        {
            if (_transaction is null) return;
            await _transaction.RollbackAsync();
            await _connection.CloseAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }

            if (_connection.State == ConnectionState.Open)
            {
                await _connection.CloseAsync();
            }

            await _connection.DisposeAsync();
        }
    }
}