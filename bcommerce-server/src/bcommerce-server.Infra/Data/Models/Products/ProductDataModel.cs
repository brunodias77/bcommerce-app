using System.Text.Json;
using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.ValueObjects;

namespace bcommerce_server.Infra.Data.Models.Products;

public sealed record ProductDataModel(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    decimal? OldPrice,
    CategoryID CategoryId,
    int StockQuantity,
    int Sold,
    bool IsActive,
    bool Popular,
    DateTime? DeletedAt,
    DateTime CreatedAt,
    DateTime UpdatedAt
);