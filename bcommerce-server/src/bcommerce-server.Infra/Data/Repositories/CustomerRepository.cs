using Dapper;
using Npgsql;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Customers.Identifiers;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.Addresses.Identifiers;
using bcommerce_server.Infra.Repositories; // 🆕
using Address = bcommerce_server.Domain.Addresses.Address;

namespace bcommerce_server.Infra.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Insert(Customer customer, CancellationToken cancellationToken)
    {
        // 🔄 ALTERADO: campo `deleted` ➡️ `deleted_at`
        const string insertCustomerSql = @"
            INSERT INTO customers 
                (id, name, email, password, cpf, deleted_at, created_at, updated_at)
            VALUES 
                (@Id, @Name, @Email, @Password, @Cpf, @DeletedAt, @CreatedAt, @UpdatedAt)";

        const string insertAddressSql = @"
            INSERT INTO customer_addresses 
                (id, customer_id, street, number, city, state, zip_code, created_at)
            VALUES 
                (@Id, @CustomerId, @Street, @Number, @City, @State, @ZipCode, @CreatedAt)";

        var conn = _unitOfWork.Connection;
        var trans = _unitOfWork.Transaction;

        await conn.ExecuteAsync(insertCustomerSql, new
        {
            Id = customer.Id.Value,
            customer.Name,
            Email = customer.Email.Address,
            Password = customer.Password,
            Cpf = customer.Cpf?.Number,
            DeletedAt = customer.DeletedAt, // 🆕 AGORA É DateTime? (nullable)
            customer.CreatedAt,
            customer.UpdatedAt
        }, trans);

        foreach (var address in customer.Addresses)
        {
            await conn.ExecuteAsync(insertAddressSql, new
            {
                Id = address.Id.Value,
                CustomerId = customer.Id.Value,
                address.Street,
                address.Number,
                address.City,
                address.State,
                address.ZipCode,
                address.CreatedAt
            }, trans);
        }
    }

    // 📌 MÉTODOS A IMPLEMENTAR (sem mudanças neste momento)
    public Task<Customer> Get(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Customer aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(Customer aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    // TODO
    public async Task<Customer> GetByEmail(Email email, CancellationToken cancellationToken)
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

        var customerData = await conn.QueryFirstOrDefaultAsync(customerSql, new
        {
            Email = email.Address
        }, trans);

        if (customerData is null)
            return null;

        var customerId = new CustomerID(customerData.id);

        // Executa a query de endereços, MAS pode retornar zero registros
        var addressData = (await conn.QueryAsync(addressSql, new
        {
            CustomerId = customerId.Value
        }, trans)).ToList();

        // Cria lista de endereços apenas se houver dados
        List<Address>? addresses = addressData.Any()
            ? addressData.Select(addr => Address.With(
                new AddressID(addr.id),
                new CustomerID(addr.customer_id),
                addr.street,
                addr.number,
                addr.city,
                addr.state,
                addr.zip_code,
                addr.created_at,
                addr.updated_at
            )).ToList()
            : null; // ✅ Aqui está o diferencial: null se não tiver endereços

        var customer = Customer.With(
            id: customerId,
            name: customerData.name,
            email: new Email(customerData.email),
            password: customerData.password,
            cpf: customerData.cpf is not null ? new Cpf(customerData.cpf) : null,
            addresses: addresses,
            deletedAt: customerData.deleted_at,
            createdAt: customerData.created_at,
            updatedAt: customerData.updated_at
        );

        return customer;
    }


    public Task<Customer> GetByCpf(string cpf, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
