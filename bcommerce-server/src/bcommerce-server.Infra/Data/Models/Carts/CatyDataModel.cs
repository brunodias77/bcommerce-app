namespace bcommerce_server.Infra.Data.Models.Carts;

public sealed record CartDataModel(
    Guid Id,
    Guid CustomerId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);