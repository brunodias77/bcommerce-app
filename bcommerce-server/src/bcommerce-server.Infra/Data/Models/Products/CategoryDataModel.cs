namespace bcommerce_server.Infra.Data.Models.Products;

public record CategoryDataModel(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt
);