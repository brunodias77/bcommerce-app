using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcommerce_server.Application.Products.GetAll;

public sealed record GetAllProductItemOutput(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    decimal? OldPrice,
    Guid CategoryId,
    string? CategoryName, // ✅ nova propriedade
    int StockQuantity,
    int Sold,
    bool IsActive,
    bool Popular,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<string> Images,
    List<string> Colors,
    List<ReviewItemOutput> Reviews
);

public sealed record ReviewItemOutput(
    int Rating,
    string? Comment,
    DateTime CreatedAt
);

public sealed record GetAllProductsOutput(
    List<GetAllProductItemOutput> Products
);

