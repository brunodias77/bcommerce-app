using bcommerce_server.Domain.Customers.Identifiers;

namespace bcommerce_server.Domain.Customers.Gateways;

public interface ICustomerGateway
{
    Task<Customer> GetById(CustomerID id, CancellationToken cancellationToken);
    Task Add(Customer customer, CancellationToken cancellationToken);
    Task Update(Customer customer, CancellationToken cancellationToken);
    Task Delete(Customer customer, CancellationToken cancellationToken);
}