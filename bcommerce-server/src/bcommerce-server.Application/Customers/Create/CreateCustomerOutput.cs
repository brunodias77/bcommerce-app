using bcommerce_server.Domain.Categories;
using bcommerce_server.Domain.Customers;

namespace bcommerce_server.Application.Customers.Create;

public sealed record CreateCustomerOutput(string Name)
{
    public static CreateCustomerOutput From(Customer customer)
        => new(customer.Name);
}