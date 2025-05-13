using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Entities;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Infra.Data.Models.Customers;
using Dapper;

namespace bcommerce_server.Infra.Repositories;

public class DapperCustomerRepository : ICustomerRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public DapperCustomerRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Insert(Customer aggregate, CancellationToken cancellationToken)
    {
        var model = CustomerMapper.ToDataModel(aggregate);
        const string sql = @"
            INSERT INTO customers (id, name, email, password, cpf, deleted_at, created_at, updated_at)
            VALUES (@Id, @Name, @Email, @Password, @Cpf, @DeletedAt, @CreatedAt, @UpdatedAt);
        ";

        await _unitOfWork.Connection.ExecuteAsync(sql, model, _unitOfWork.Transaction);
    }

    public async Task<Customer?> Get(Guid id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM customers WHERE id = @Id;";
        var model = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<CustomerDataModel>(sql, new { Id = id }, _unitOfWork.Transaction);

        if (model == null) return null;

        // Você pode incluir Address, caso queira, via JOIN ou outro método auxiliar
        return CustomerMapper.ToDomain(model, new List<CustomerAddress>()); // ← Por enquanto, sem endereços
    }

    public async Task<IEnumerable<Customer>> GetAll(CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM customers;";
        var models = await _unitOfWork.Connection.QueryAsync<CustomerDataModel>(sql, transaction: _unitOfWork.Transaction);

        return models.Select(m => CustomerMapper.ToDomain(m, new List<CustomerAddress>()));
    }

    public async Task Update(Customer aggregate, CancellationToken cancellationToken)
    {
        var model = CustomerMapper.ToDataModel(aggregate);
        const string sql = @"
            UPDATE customers 
            SET name = @Name, email = @Email, password = @Password, cpf = @Cpf, deleted_at = @DeletedAt, updated_at = @UpdatedAt
            WHERE id = @Id;
        ";

        await _unitOfWork.Connection.ExecuteAsync(sql, model, _unitOfWork.Transaction);
    }

    public async Task Delete(Customer aggregate, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM customers WHERE id = @Id;";
        await _unitOfWork.Connection.ExecuteAsync(sql, new { Id = aggregate.Id.Value }, _unitOfWork.Transaction);
    }


    public async Task<Customer?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM customers WHERE email = @Email;";
        var model = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<CustomerDataModel>(sql, new { Email = email }, _unitOfWork.Transaction);

        return model == null
            ? null
            : CustomerMapper.ToDomain(model, new List<CustomerAddress>());
    }

    public Task<Customer> GetByCpf(string cpf, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


}