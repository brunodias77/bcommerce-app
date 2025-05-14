namespace bcommerce_server.Infra.Data.Models.Products;

public sealed record ProductColorDataModel(
    Guid ProductId,
    Guid ColorId,
    string ColorName,
    string ColorValue,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
