using bcommerce_server.Domain.Addresses;
using bcommerce_server.Domain.Addresses.Identifiers;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Identifiers;
using bcommerce_server.Domain.Customers.ValueObjects;

namespace bcommerce_server.Infra.Data.Models.Customers;

public static class CustomerMapper
{
    public static Customer ToDomain(this CustomerDataModel model)
    {
        var addresses = model.Addresses?.Select(a =>
            Address.With(
                 AddressID.From(a.Id),
                 CustomerID.From(a.CustomerId),
                a.Street,
                a.Number,
                a.City,
                a.State,
                a.ZipCode,
                a.CreatedAt,
                a.UpdatedAt
            )).ToList();

        return Customer.With(
            CustomerID.From(model.Id),
            model.Name,
            Email.From(model.Email),
            model.Password,
            model.Cpf != null ?  Cpf.From(model.Cpf) : null,
            addresses,
            model.DeletedAt,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public static CustomerDataModel ToDataModel(this Customer customer)
    {
        return new CustomerDataModel
        {
            Id = customer.Id.Value,
            Name = customer.Name,
            Email = customer.Email.Address,
            Password = customer.Password,
            Cpf = customer.Cpf?.Number,
            DeletedAt = customer.DeletedAt,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt,
            Addresses = customer.Addresses.Select(a => new CustomerAddressDataModel
            {
                Id = a.Id.Value,
                CustomerId = a.CustomerId.Value,
                Street = a.Street,
                Number = a.Number,
                City = a.City,
                State = a.State,
                ZipCode = a.ZipCode,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            }).ToList()
        };
    }
}