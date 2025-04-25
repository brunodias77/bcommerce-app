namespace bcommerce_server.Infra.Data.Models.Products;

public sealed record ProductColorDataModel(
    Guid Id,
    Guid ProductId,
    string ColorValue,
    DateTime CreatedAt
);