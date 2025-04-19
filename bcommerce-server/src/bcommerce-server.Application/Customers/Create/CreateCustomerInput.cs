namespace bcommerce_server.Application.Customers.Create;

public sealed record CreateCustomerInput(
    string Name,
    string Email,
    string Cpf,
    List<AddressRequest> Addresses
);

public sealed record AddressRequest(
    string Street,
    string Number,
    string City,
    string State,
    string ZipCode
);