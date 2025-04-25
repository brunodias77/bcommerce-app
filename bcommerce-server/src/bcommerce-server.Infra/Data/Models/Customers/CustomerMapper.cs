using bcommerce_server.Domain.Addresses;
using bcommerce_server.Domain.Addresses.Identifiers;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Entities;
using bcommerce_server.Domain.Customers.Identifiers;
using bcommerce_server.Domain.Customers.ValueObjects;

namespace bcommerce_server.Infra.Data.Models.Customers;

public static class CustomerMapper
{
    public static Customer ToDomain(CustomerDataModel model, List<CustomerAddress> addresses)
    {
        return Customer.With(
            CustomerID.From(model.Id),
            model.Name,
            Email.From(model.Email),
            model.Password,
            model.Cpf is not null ? Cpf.From(model.Cpf) : null,
            addresses,
            model.DeletedAt,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public static CustomerDataModel ToDataModel(Customer customer)
    {
        return new CustomerDataModel(
            customer.Id.Value,
            customer.Name,
            customer.Email.Address,
            customer.Password,
            customer.Cpf?.Number,
            customer.DeletedAt,
            customer.CreatedAt,
            customer.UpdatedAt
        );
    }
}