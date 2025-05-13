using bcommerce_server.Domain.Customers;

namespace bcommerce_server.Domain.Services;

public interface ILoggedCustomer
{
    public Task<Customer> User();

}