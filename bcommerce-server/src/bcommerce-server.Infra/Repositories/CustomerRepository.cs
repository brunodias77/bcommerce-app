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
        const string insertCustomerSql = @"
            INSERT INTO customers (id, name, email, password, cpf, deleted, created_at, updated_at)
            VALUES (@Id, @Name, @Email, @Password, @Cpf, @Deleted, @CreatedAt, @UpdatedAt)";

        const string insertAddressSql = @"
            INSERT INTO customer_addresses (id, customer_id, street, number, city, state, zip_code, created_at)
            VALUES (@Id, @CustomerId, @Street, @Number, @City, @State, @ZipCode, @CreatedAt)";

        var conn = _unitOfWork.Connection;
        var trans = _unitOfWork.Transaction;

        await conn.ExecuteAsync(insertCustomerSql, new
        {
            Id = customer.Id.Value,
            customer.Name,
            Email = customer.Email.Address,
            Password = customer.Password,
            Cpf = customer.Cpf?.Number,
            Deleted = customer.IsDeleted,
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

    public async Task<Customer?> Get(Guid id, CancellationToken cancellationToken)
    {
        const string customerSql = "SELECT * FROM customers WHERE id = @Id";
        const string addressSql = "SELECT * FROM customer_addresses WHERE customer_id = @CustomerId";

        var conn = _unitOfWork.Connection;

        var customerData = await conn.QueryFirstOrDefaultAsync(customerSql, new { Id = id });

        if (customerData is null)
            return null;

        var addresses = await conn.QueryAsync(addressSql, new { CustomerId = id });

        var addressList = addresses.Select(a => Address.With(
            AddressID.From((Guid)a.id),
            CustomerID.From((Guid)a.customer_id),
            (string)a.street,
            (string)a.number,
            (string)a.city,
            (string)a.state,
            (string)a.zip_code,
            (DateTime)a.created_at
        )).ToList();

        return Customer.With(
            CustomerID.From((Guid)customerData.id),
            (string)customerData.name,
            Email.From((string)customerData.email),
            (string)customerData.password,
            string.IsNullOrWhiteSpace((string?)customerData.cpf) ? null : Cpf.From((string)customerData.cpf),
            addressList,
            (bool)customerData.deleted,
            (DateTime)customerData.created_at,
            (DateTime)customerData.updated_at
        );
    }

    public async Task Update(Customer customer, CancellationToken cancellationToken)
    {
        const string updateCustomerSql = @"
            UPDATE customers 
            SET name = @Name, email = @Email, password = @Password, cpf = @Cpf, deleted = @Deleted, updated_at = @UpdatedAt
            WHERE id = @Id";

        const string deleteAddressesSql = @"DELETE FROM customer_addresses WHERE customer_id = @CustomerId";

        const string insertAddressSql = @"
            INSERT INTO customer_addresses (id, customer_id, street, number, city, state, zip_code, created_at)
            VALUES (@Id, @CustomerId, @Street, @Number, @City, @State, @ZipCode, @CreatedAt)";

        var conn = _unitOfWork.Connection;
        var trans = _unitOfWork.Transaction;

        await conn.ExecuteAsync(updateCustomerSql, new
        {
            Id = customer.Id.Value,
            customer.Name,
            Email = customer.Email.Address,
            Password = customer.Password,
            Cpf = customer.Cpf?.Number,
            Deleted = customer.IsDeleted,
            customer.UpdatedAt
        }, trans);

        await conn.ExecuteAsync(deleteAddressesSql, new
        {
            CustomerId = customer.Id.Value
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

    public async Task Delete(Customer customer, CancellationToken cancellationToken)
    {
        const string query = "DELETE FROM customers WHERE id = @Id";

        var conn = _unitOfWork.Connection;

        await conn.ExecuteAsync(query, new { Id = customer.Id.Value });
    }

    public async Task<Customer?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        const string query = "SELECT id FROM customers WHERE email = @Email LIMIT 1";

        var conn = _unitOfWork.Connection;

        var customerId = await conn.QueryFirstOrDefaultAsync<Guid?>(query, new { Email = email });

        return customerId.HasValue
            ? await Get(customerId.Value, cancellationToken)
            : null;
    }

    public async Task<Customer?> GetByCpf(string cpf, CancellationToken cancellationToken)
    {
        const string query = "SELECT id FROM customers WHERE cpf = @Cpf LIMIT 1";

        var conn = _unitOfWork.Connection;

        var customerId = await conn.QueryFirstOrDefaultAsync<Guid?>(query, new { Cpf = cpf });

        return customerId.HasValue
            ? await Get(customerId.Value, cancellationToken)
            : null;
    }
}



// using Dapper;
// using Npgsql;
// using bcommerce_server.Domain.Customers;
// using bcommerce_server.Domain.Customers.Repositories;
// using bcommerce_server.Domain.Customers.Identifiers;
// using bcommerce_server.Domain.Customers.ValueObjects;
// using bcommerce_server.Domain.Addresses.Identifiers;
// using Microsoft.Extensions.Configuration;
// using Address = bcommerce_server.Domain.Addresses.Address;
//
// namespace bcommerce_server.Infra.Repositories;
//
// public class CustomerRepository : ICustomerRepository
// {
//     private readonly string _connectionString;
//
//     public CustomerRepository(IConfiguration configuration)
//     {
//         _connectionString = configuration.GetConnectionString("DefaultConnection");
//     }
//
//     public async Task Insert(Customer customer, CancellationToken cancellationToken)
//     {
//         const string insertCustomerSql = @"
//             INSERT INTO customers (id, name, email, password, cpf, deleted, created_at, updated_at) -- 🆕 Adicionou password
//             VALUES (@Id, @Name, @Email, @Password, @Cpf, @Deleted, @CreatedAt, @UpdatedAt)"; // 🔄 cpf pode ser null
//
//         const string insertAddressSql = @"
//             INSERT INTO customer_addresses (id, customer_id, street, number, city, state, zip_code, created_at)
//             VALUES (@Id, @CustomerId, @Street, @Number, @City, @State, @ZipCode, @CreatedAt)";
//
//         using var connection = CreateConnection();
//         await connection.OpenAsync(cancellationToken);
//         using var transaction = await connection.BeginTransactionAsync(cancellationToken);
//
//         try
//         {
//             await connection.ExecuteAsync(insertCustomerSql, new
//             {
//                 Id = Guid.Parse(customer.Id.Value),
//                 customer.Name,
//                 Email = customer.Email.Address,
//                 Password = customer.Password, // 🆕
//                 Cpf = customer.Cpf?.Number,    // 🔄 null-safe
//                 Deleted = customer.IsDeleted,
//                 customer.CreatedAt,
//                 customer.UpdatedAt
//             }, transaction);
//
//             foreach (var address in customer.Addresses)
//             {
//                 await connection.ExecuteAsync(insertAddressSql, new
//                 {
//                     Id = Guid.Parse(address.Id.Value),
//                     CustomerId = Guid.Parse(customer.Id.Value),
//                     address.Street,
//                     address.Number,
//                     address.City,
//                     address.State,
//                     address.ZipCode,
//                     address.CreatedAt
//                 }, transaction);
//             }
//
//             await transaction.CommitAsync(cancellationToken);
//         }
//         catch
//         {
//             await transaction.RollbackAsync(cancellationToken);
//             throw;
//         }
//     }
//
//     public async Task<Customer?> Get(Guid id, CancellationToken cancellationToken)
//     {
//         const string customerSql = "SELECT * FROM customers WHERE id = @Id";
//         const string addressSql = "SELECT * FROM customer_addresses WHERE customer_id = @CustomerId";
//
//         using var connection = CreateConnection();
//         await connection.OpenAsync(cancellationToken);
//
//         var customerData = await connection.QueryFirstOrDefaultAsync(customerSql, new { Id = id });
//
//         if (customerData is null)
//             return null;
//
//         var addresses = await connection.QueryAsync(addressSql, new { CustomerId = id });
//
//         var addressList = addresses.Select(a => Address.With(
//             AddressID.From(((Guid)a.id).ToString("N").ToLowerInvariant()),
//             CustomerID.From(((Guid)a.customer_id).ToString("N").ToLowerInvariant()),
//             (string)a.street,
//             (string)a.number,
//             (string)a.city,
//             (string)a.state,
//             (string)a.zip_code,
//             (DateTime)a.created_at
//         )).ToList();
//
//         return Customer.With(
//             CustomerID.From(((Guid)customerData.id).ToString("N").ToLowerInvariant()),
//             (string)customerData.name,
//             Email.From((string)customerData.email),
//             (string)customerData.password, // 🆕 leitura do campo password
//             string.IsNullOrWhiteSpace((string?)customerData.cpf) ? null : Cpf.From((string)customerData.cpf), // 🔄 cpf opcional
//             addressList,
//             (bool)customerData.deleted,
//             (DateTime)customerData.created_at,
//             (DateTime)customerData.updated_at
//         );
//     }
//
//     public async Task Update(Customer customer, CancellationToken cancellationToken)
//     {
//         const string updateCustomerSql = @"
//             UPDATE customers 
//             SET name = @Name, email = @Email, password = @Password, cpf = @Cpf, deleted = @Deleted, updated_at = @UpdatedAt
//             WHERE id = @Id"; // 🔄 adicionou password
//
//         const string deleteAddressesSql = @"DELETE FROM customer_addresses WHERE customer_id = @CustomerId";
//
//         const string insertAddressSql = @"
//             INSERT INTO customer_addresses (id, customer_id, street, number, city, state, zip_code, created_at)
//             VALUES (@Id, @CustomerId, @Street, @Number, @City, @State, @ZipCode, @CreatedAt)";
//
//         using var connection = CreateConnection();
//         await connection.OpenAsync(cancellationToken);
//         using var transaction = await connection.BeginTransactionAsync(cancellationToken);
//
//         try
//         {
//             await connection.ExecuteAsync(updateCustomerSql, new
//             {
//                 Id = Guid.Parse(customer.Id.Value),
//                 customer.Name,
//                 Email = customer.Email.Address,
//                 Password = customer.Password, // 🆕
//                 Cpf = customer.Cpf?.Number,   // 🔄 cpf opcional
//                 Deleted = customer.IsDeleted,
//                 customer.UpdatedAt
//             }, transaction);
//
//             await connection.ExecuteAsync(deleteAddressesSql, new
//             {
//                 CustomerId = Guid.Parse(customer.Id.Value)
//             }, transaction);
//
//             foreach (var address in customer.Addresses)
//             {
//                 await connection.ExecuteAsync(insertAddressSql, new
//                 {
//                     Id = Guid.Parse(address.Id.Value),
//                     CustomerId = Guid.Parse(customer.Id.Value),
//                     address.Street,
//                     address.Number,
//                     address.City,
//                     address.State,
//                     address.ZipCode,
//                     address.CreatedAt
//                 }, transaction);
//             }
//
//             await transaction.CommitAsync(cancellationToken);
//         }
//         catch
//         {
//             await transaction.RollbackAsync(cancellationToken);
//             throw;
//         }
//     }
//
//     public async Task Delete(Customer customer, CancellationToken cancellationToken)
//     {
//         const string query = "DELETE FROM customers WHERE id = @Id";
//
//         using var connection = CreateConnection();
//         await connection.ExecuteAsync(query, new { Id = Guid.Parse(customer.Id.Value) });
//     }
//
//     public async Task<Customer?> GetByEmail(string email, CancellationToken cancellationToken)
//     {
//         const string query = "SELECT id FROM customers WHERE email = @Email LIMIT 1";
//         var customerId = await ExecuteQueryAsync(async conn =>
//             await conn.QueryFirstOrDefaultAsync<Guid?>(query, new { Email = email }), cancellationToken);
//
//         return customerId.HasValue
//             ? await Get(customerId.Value, cancellationToken)
//             : null;
//     }
//
//     public async Task<Customer?> GetByCpf(string cpf, CancellationToken cancellationToken)
//     {
//         const string query = "SELECT id FROM customers WHERE cpf = @Cpf LIMIT 1";
//         var customerId = await ExecuteQueryAsync(async conn =>
//             await conn.QueryFirstOrDefaultAsync<Guid?>(query, new { Cpf = cpf }), cancellationToken);
//
//         return customerId.HasValue
//             ? await Get(customerId.Value, cancellationToken)
//             : null;
//     }
//
//     // Helpers
//     private async Task<T> ExecuteQueryAsync<T>(Func<NpgsqlConnection, Task<T>> queryFunction, CancellationToken cancellationToken)
//     {
//         using var connection = CreateConnection();
//         await connection.OpenAsync(cancellationToken);
//         try
//         {
//             return await queryFunction(connection);
//         }
//         catch (Exception ex)
//         {
//             throw new InvalidOperationException("Erro ao executar a consulta no banco de dados.", ex);
//         }
//     }
//
//     private NpgsqlConnection CreateConnection()
//         => new(_connectionString);
// }
//
// // using Dapper;
// // using Npgsql;
// // using bcommerce_server.Domain.Customers;
// // using bcommerce_server.Domain.Customers.Repositories;
// // using bcommerce_server.Domain.Customers.Identifiers;
// // using bcommerce_server.Domain.Customers.ValueObjects;
// // using bcommerce_server.Domain.Addresses.Identifiers;
// // using Microsoft.Extensions.Configuration;
// // using Address = bcommerce_server.Domain.Addresses.Address;
// //
// // namespace bcommerce_server.Infra.Repositories;
// //
// // public class CustomerRepository : ICustomerRepository
// // {
// //     private readonly string _connectionString;
// //
// //     public CustomerRepository(IConfiguration configuration)
// //     {
// //         _connectionString = configuration.GetConnectionString("DefaultConnection");
// //     }
// //
// //     public async Task Insert(Customer customer, CancellationToken cancellationToken)
// //     {
// //         const string insertCustomerSql = @"
// //             INSERT INTO customers (id, name, email, cpf, deleted, created_at, updated_at)
// //             VALUES (@Id, @Name, @Email, @Cpf, @Deleted, @CreatedAt, @UpdatedAt)";
// //
// //         const string insertAddressSql = @"
// //             INSERT INTO customer_addresses (id, customer_id, street, number, city, state, zip_code, created_at)
// //             VALUES (@Id, @CustomerId, @Street, @Number, @City, @State, @ZipCode, @CreatedAt)";
// //
// //         using var connection = CreateConnection();
// //         await connection.OpenAsync(cancellationToken);
// //         using var transaction = await connection.BeginTransactionAsync(cancellationToken);
// //
// //         try
// //         {
// //             await connection.ExecuteAsync(insertCustomerSql, new
// //             {
// //                 Id = Guid.Parse(customer.Id.Value),
// //                 customer.Name,
// //                 Email = customer.Email.Address,
// //                 Cpf = customer.Cpf.Number,
// //                 Deleted = customer.IsDeleted,
// //                 customer.CreatedAt,
// //                 customer.UpdatedAt
// //             }, transaction);
// //
// //             foreach (var address in customer.Addresses)
// //             {
// //                 await connection.ExecuteAsync(insertAddressSql, new
// //                 {
// //                     Id = Guid.Parse(address.Id.Value),
// //                     CustomerId = Guid.Parse(customer.Id.Value),
// //                     address.Street,
// //                     address.Number,
// //                     address.City,
// //                     address.State,
// //                     address.ZipCode,
// //                     address.CreatedAt
// //                 }, transaction);
// //             }
// //
// //             await transaction.CommitAsync(cancellationToken);
// //         }
// //         catch
// //         {
// //             await transaction.RollbackAsync(cancellationToken);
// //             throw;
// //         }
// //     }
// //
// //     public async Task<Customer?> Get(Guid id, CancellationToken cancellationToken)
// //     {
// //         const string customerSql = "SELECT * FROM customers WHERE id = @Id";
// //         const string addressSql = "SELECT * FROM customer_addresses WHERE customer_id = @CustomerId";
// //
// //         using var connection = CreateConnection();
// //         await connection.OpenAsync(cancellationToken);
// //
// //         var customerData = await connection.QueryFirstOrDefaultAsync(customerSql, new { Id = id });
// //
// //         if (customerData is null)
// //             return null;
// //
// //         var addresses = await connection.QueryAsync(addressSql, new { CustomerId = id });
// //
// //         var addressList = addresses.Select(a => Address.With(
// //             AddressID.From(((Guid)a.id).ToString("N").ToLowerInvariant()),
// //             CustomerID.From(((Guid)a.customer_id).ToString("N").ToLowerInvariant()),
// //             (string)a.street,
// //             (string)a.number,
// //             (string)a.city,
// //             (string)a.state,
// //             (string)a.zip_code,
// //             (DateTime)a.created_at
// //         )).ToList();
// //
// //         return Customer.With(
// //             CustomerID.From(((Guid)customerData.id).ToString("N").ToLowerInvariant()),
// //             (string)customerData.name,
// //             Email.From((string)customerData.email),
// //             Cpf.From((string)customerData.cpf),
// //             addressList,
// //             (bool)customerData.deleted,
// //             (DateTime)customerData.created_at,
// //             (DateTime)customerData.updated_at
// //         );
// //     }
// //
// //     public async Task Update(Customer customer, CancellationToken cancellationToken)
// //     {
// //         const string updateCustomerSql = @"
// //             UPDATE customers 
// //             SET name = @Name, email = @Email, cpf = @Cpf, deleted = @Deleted, updated_at = @UpdatedAt
// //             WHERE id = @Id";
// //
// //         const string deleteAddressesSql = @"DELETE FROM customer_addresses WHERE customer_id = @CustomerId";
// //
// //         const string insertAddressSql = @"
// //             INSERT INTO customer_addresses (id, customer_id, street, number, city, state, zip_code, created_at)
// //             VALUES (@Id, @CustomerId, @Street, @Number, @City, @State, @ZipCode, @CreatedAt)";
// //
// //         using var connection = CreateConnection();
// //         await connection.OpenAsync(cancellationToken);
// //         using var transaction = await connection.BeginTransactionAsync(cancellationToken);
// //
// //         try
// //         {
// //             await connection.ExecuteAsync(updateCustomerSql, new
// //             {
// //                 Id = Guid.Parse(customer.Id.Value),
// //                 customer.Name,
// //                 Email = customer.Email.Address,
// //                 Cpf = customer.Cpf.Number,
// //                 Deleted = customer.IsDeleted,
// //                 customer.UpdatedAt
// //             }, transaction);
// //
// //             await connection.ExecuteAsync(deleteAddressesSql, new
// //             {
// //                 CustomerId = Guid.Parse(customer.Id.Value)
// //             }, transaction);
// //
// //             foreach (var address in customer.Addresses)
// //             {
// //                 await connection.ExecuteAsync(insertAddressSql, new
// //                 {
// //                     Id = Guid.Parse(address.Id.Value),
// //                     CustomerId = Guid.Parse(customer.Id.Value),
// //                     address.Street,
// //                     address.Number,
// //                     address.City,
// //                     address.State,
// //                     address.ZipCode,
// //                     address.CreatedAt
// //                 }, transaction);
// //             }
// //
// //             await transaction.CommitAsync(cancellationToken);
// //         }
// //         catch
// //         {
// //             await transaction.RollbackAsync(cancellationToken);
// //             throw;
// //         }
// //     }
// //
// //     public async Task Delete(Customer customer, CancellationToken cancellationToken)
// //     {
// //         const string query = "DELETE FROM customers WHERE id = @Id";
// //
// //         using var connection = CreateConnection();
// //         await connection.ExecuteAsync(query, new { Id = Guid.Parse(customer.Id.Value) });
// //     }
// //
// //     public async Task<Customer?> GetByEmail(string email, CancellationToken cancellationToken)
// //     {
// //         const string query = "SELECT id FROM customers WHERE email = @Email LIMIT 1";
// //         var customerId = await ExecuteQueryAsync(async conn =>
// //             await conn.QueryFirstOrDefaultAsync<Guid?>(query, new { Email = email }), cancellationToken);
// //
// //         return customerId.HasValue
// //             ? await Get(customerId.Value, cancellationToken)
// //             : null;
// //     }
// //
// //     public async Task<Customer?> GetByCpf(string cpf, CancellationToken cancellationToken)
// //     {
// //         const string query = "SELECT id FROM customers WHERE cpf = @Cpf LIMIT 1";
// //         var customerId = await ExecuteQueryAsync(async conn =>
// //             await conn.QueryFirstOrDefaultAsync<Guid?>(query, new { Cpf = cpf }), cancellationToken);
// //
// //         return customerId.HasValue
// //             ? await Get(customerId.Value, cancellationToken)
// //             : null;
// //     }
// //
// //     // Helpers
// //     private async Task<T> ExecuteQueryAsync<T>(Func<NpgsqlConnection, Task<T>> queryFunction, CancellationToken cancellationToken)
// //     {
// //         using var connection = CreateConnection();
// //         await connection.OpenAsync(cancellationToken);
// //         try
// //         {
// //             return await queryFunction(connection);
// //         }
// //         catch (Exception ex)
// //         {
// //             throw new InvalidOperationException("Erro ao executar a consulta no banco de dados.", ex);
// //         }
// //     }
// //
// //     private NpgsqlConnection CreateConnection()
// //         => new(_connectionString);
// // }
