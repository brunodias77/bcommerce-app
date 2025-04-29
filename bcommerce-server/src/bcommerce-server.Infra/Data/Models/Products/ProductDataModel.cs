using System.Text.Json;
using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.ValueObjects;

namespace bcommerce_server.Infra.Data.Models.Products;

public class ProductDataModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public Guid CategoryId { get; set; } // <-- CUIDADO, CategoryId é GUID puro no banco
    public int StockQuantity { get; set; }
    public int Sold { get; set; }
    public bool IsActive { get; set; }
    public bool Popular { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; } // Pode ser NULL

    public ProductDataModel() { }
}