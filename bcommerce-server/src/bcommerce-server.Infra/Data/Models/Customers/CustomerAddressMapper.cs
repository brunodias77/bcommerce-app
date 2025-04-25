using bcommerce_server.Domain.Customers.Entities;
using bcommerce_server.Domain.Customers.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Customers;

public static class CustomerAddressMapper
{
    public static CustomerAddress ToDomain(CustomerAddressDataModel model)
    {
        return CustomerAddress.With(
            CustomerAddressID.From(model.Id),
            model.Street,
            model.Number,
            model.City,
            model.State,
            model.ZipCode,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public static CustomerAddressDataModel ToDataModel(CustomerAddress address, Guid customerId)
    {
        return new CustomerAddressDataModel(
            address.Id.Value,
            customerId,
            address.Street,
            address.Number,
            address.City,
            address.State,
            address.ZipCode,
            address.CreatedAt,
            address.UpdatedAt
        );
    }
}