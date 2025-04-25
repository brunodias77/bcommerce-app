using bcommerce_server.Domain.Customers.Identifiers;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Customers.Repositories;

public interface ICustomerRepository : IGenericRepository<Customer> {
    Task<Customer?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Customer> GetByCpf(string cpf, CancellationToken cancellationToken);
}