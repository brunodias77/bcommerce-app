namespace bcommerce_server.Infra.Data.Models.Products;

public sealed record ProductImageDataModel(
    Guid Id,
    Guid ProductId,
    string Url,
    DateTime CreatedAt
);