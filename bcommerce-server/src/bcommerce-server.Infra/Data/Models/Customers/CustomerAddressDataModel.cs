namespace bcommerce_server.Infra.Data.Models.Customers;

public sealed record CustomerAddressDataModel(
    Guid Id,
    Guid CustomerId,
    string Street,
    string Number,
    string City,
    string State,
    string ZipCode,
    DateTime CreatedAt,
    DateTime UpdatedAt
);