using System;
using System.Collections.Generic;
using bcommerce_server.Application.Products.GetAll;

namespace bcommerce_server.Application.Products.GetById;

public record GetProductByIdOutput(
        Guid ProductId,
        string Name,
        string Description,
        decimal Price,
        decimal? OldPrice,
        Guid CategoryId,
        string? CategoryName,
        int StockQuantity,
        int Sold,
        bool IsActive,
        bool Popular,
        DateTime CreatedAt,
        List<string> Images,
        List<ColorItemOutput> Colors,
        List<ReviewItemOutput> Reviews
);

public sealed record ColorItemOutput(
    string Name,
    string Value
);

public sealed record ReviewItemOutput(
    int Rating,
    string? Comment,
    DateTime CreatedAt
);