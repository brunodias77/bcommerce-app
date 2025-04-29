namespace bcommerce_server.Infra.Data.Models.Products;

public sealed record ProductReviewDataModel(
    Guid Id,
    Guid ProductId,     // <-- adicionar
    Guid CustomerId,    // <-- adicionar
    int Rating,
    string? Comment,
    DateTime CreatedAt,
    DateTime UpdatedAt
);