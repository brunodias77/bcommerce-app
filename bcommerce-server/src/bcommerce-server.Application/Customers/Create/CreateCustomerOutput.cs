using bcommerce_server.Domain.Customers;

namespace bcommerce_server.Application.Customers.Create;

public record CreateCustomerOutput(Guid Id, string Name, string Email, bool IsActive, DateTime CreatedAt)
{
    public static CreateCustomerOutput FromCustomer(Customer customer) =>
        new(customer.Id.Value, customer.Name, customer.Email.Address, customer.IsActive, customer.CreatedAt);
}