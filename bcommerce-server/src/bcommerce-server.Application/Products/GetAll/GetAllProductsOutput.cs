using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcommerce_server.Application.Products.GetAll;

public sealed record GetAllProductsOutput(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    decimal? OldPrice,
    Guid CategoryId,        // ← Aqui está o argumento que está faltando
    int StockQuantity,
    int Sold,
    bool IsActive,
    bool Popular,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
