using Dapper;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Infra.Data.Models.Customers;
using bcommerce_server.Infra.Repositories;

namespace bcommerce_server.Infra.Data.Repositories;


public class CustomerRepository : ICustomerRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Insert(Customer customer, CancellationToken cancellationToken)
    {
        var data = customer.ToDataModel();

        const string insertCustomerSql = @"
            INSERT INTO customers 
                (id, name, email, password, cpf, deleted_at, created_at, updated_at)
            VALUES 
                (@Id, @Name, @Email, @Password, @Cpf, @DeletedAt, @CreatedAt, @UpdatedAt)";

        const string insertAddressSql = @"
            INSERT INTO customer_addresses 
                (id, customer_id, street, number, city, state, zip_code, created_at, updated_at)
            VALUES 
                (@Id, @CustomerId, @Street, @Number, @City, @State, @ZipCode, @CreatedAt, @UpdatedAt)";

        var conn = _unitOfWork.Connection;
        var trans = _unitOfWork.Transaction;

        await conn.ExecuteAsync(insertCustomerSql, new
        {
            data.Id,
            data.Name,
            data.Email,
            data.Password,
            data.Cpf,
            data.DeletedAt,
            data.CreatedAt,
            data.UpdatedAt
        }, trans);

        if (data.Addresses is not null && data.Addresses.Any())
        {
            foreach (var address in data.Addresses)
            {
                await conn.ExecuteAsync(insertAddressSql, new
                {
                    address.Id,
                    address.CustomerId,
                    address.Street,
                    address.Number,
                    address.City,
                    address.State,
                    address.ZipCode,
                    address.CreatedAt,
                    address.UpdatedAt // ✅ AGORA EXISTE NO BANCO
                }, trans);
            }
        }
    }

    public async Task<Customer> GetByEmail(string email, CancellationToken cancellationToken)
    {
        const string customerSql = @"
            SELECT id, name, email, password, cpf, deleted_at, created_at, updated_at 
            FROM customers 
            WHERE email = @Email 
            LIMIT 1";

        const string addressSql = @"
            SELECT id, customer_id, street, number, city, state, zip_code, created_at, updated_at 
            FROM customer_addresses 
            WHERE customer_id = @CustomerId";

        var conn = _unitOfWork.Connection;
        var trans = _unitOfWork.Transaction;

        var customerRow = await conn.QueryFirstOrDefaultAsync<CustomerDataModel>(
            customerSql,
            new { Email = email },
            trans);

        if (customerRow is null)
            return null;

        var addressRows = (await conn.QueryAsync<CustomerAddressDataModel>(
            addressSql,
            new { CustomerId = customerRow.Id },
            trans)).ToList();

        if (addressRows.Any())
        {
            customerRow.Addresses = addressRows;
        }

        return customerRow.ToDomain();
    }

    public Task<Customer> Get(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Customer> GetByCpf(string cpf, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task Update(Customer aggregate, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task Delete(Customer aggregate, CancellationToken cancellationToken) => throw new NotImplementedException();
}


