using System;
using System.Collections.Generic;
using bcommerce_server.Application.Products.GetAll;

namespace bcommerce_server.Application.Products.GetById;

public record GetProductByIdOutput(    
    string Name,
    string Description,
    decimal Price,
    decimal? OldPrice,
    string? CategoryName, // ✅ nova propriedade
    int StockQuantity,
    int Sold,
    bool IsActive,
    bool Popular,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<string> Images,
    List<string> Colors,
    List<ReviewItemOutput> Reviews);