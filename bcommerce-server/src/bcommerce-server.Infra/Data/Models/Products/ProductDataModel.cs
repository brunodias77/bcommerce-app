using System;
using bcommerce_server.Domain.Products.Identifiers;
namespace bcommerce_server.Infra.Data.Models.Products;

public sealed class ProductDataModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public decimal Price { get; init; }
    public decimal? OldPrice { get; init; }
    public Guid CategoryId { get; init; }
    public int StockQuantity { get; init; }
    public int Sold { get; init; }
    public bool IsActive { get; init; }
    public bool Popular { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public DateTime? DeletedAt { get; init; }

    // Parameterless constructor for Dapper
    public ProductDataModel() { }

    public ProductDataModel(
        Guid id,
        string name,
        string description,
        decimal price,
        decimal? oldPrice,
        Guid categoryId,
        int stockQuantity,
        int sold,
        bool isActive,
        bool popular,
        DateTime createdAt,
        DateTime updatedAt,
        DateTime? deletedAt)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        OldPrice = oldPrice;
        CategoryId = categoryId;
        StockQuantity = stockQuantity;
        Sold = sold;
        IsActive = isActive;
        Popular = popular;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
    }
}


// using System.Text.Json;
// using bcommerce_server.Domain.Products;
// using bcommerce_server.Domain.Products.Identifiers;
//
// namespace bcommerce_server.Infra.Data.Models.Products;
//
// public class ProductDataModel
// {
//     public Guid Id { get; set; }
//     public string Name { get; set; } = default!;
//     public string Description { get; set; } = default!;
//     public decimal Price { get; set; }
//     public decimal? OldPrice { get; set; }
//     public Guid CategoryId { get; set; } // <-- CUIDADO, CategoryId é GUID puro no banco
//     public int StockQuantity { get; set; }
//     public int Sold { get; set; }
//     public bool IsActive { get; set; }
//     public bool Popular { get; set; }
//     public DateTime CreatedAt { get; set; }
//     public DateTime UpdatedAt { get; set; }
//     public DateTime? DeletedAt { get; set; } // Pode ser NULL
//
//     public ProductDataModel() { }
// }