namespace bcommerce_server.Infra.Data.Models.Products;

public sealed record ProductReviewDataModel(
    Guid Id,
    int Rating,
    string? Comment,
    DateTime CreatedAt,
    DateTime UpdatedAt
);