namespace bcommerce_server.Infra.Data.Models.Customers;

public sealed record CustomerDataModel(
    Guid Id,
    string Name,
    string Email,
    string Password,
    string? Cpf,
    DateTime? DeletedAt,
    DateTime CreatedAt,
    DateTime UpdatedAt
);