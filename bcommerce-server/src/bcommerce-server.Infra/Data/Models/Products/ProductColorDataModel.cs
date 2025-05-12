namespace bcommerce_server.Infra.Data.Models.Products;

public sealed record ProductColorDataModel(
    Guid Id,
    Guid ProductId,
    Guid ColorId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);