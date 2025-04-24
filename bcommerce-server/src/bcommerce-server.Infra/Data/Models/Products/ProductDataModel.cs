using System.Text.Json;
using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.ValueObjects;

namespace bcommerce_server.Infra.Data.Models.Products;

public class ProductDataModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public int StockQuantity { get; set; }
    public int Sold { get; set; }
    public bool IsActive { get; set; }
    public bool Popular { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = "";

    // Essas propriedades vem como JSON string no SQL:
    public string ImagesJson { get; set; } = "[]";
    public string ColorsJson { get; set; } = "[]";

    // Propriedades computadas que o mapper vai usar
    public List<string> Images => JsonSerializer.Deserialize<List<string>>(ImagesJson) ?? new();
    public List<string> Colors => JsonSerializer.Deserialize<List<string>>(ColorsJson) ?? new();
}
