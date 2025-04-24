using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.ValueObjects;

namespace bcommerce_server.Infra.Data.Models.Products;

public static class ProductMapper
{
    public static Product ToDomain(this ProductDataModel p)
    {
        return Product.With(
            id:  ProductID.From(p.Id),
            name: p.Name,
            description: p.Description,
            price:  Price.From(p.Price),
            oldPrice: p.OldPrice is not null ?  Price.From(p.OldPrice.Value) : null,
            images: p.Images?.Select(url =>  ImageUrl.From(url)).ToList() ?? new List<ImageUrl>(),
            category: Category.From(p.CategoryName),
            colors: p.Colors?.Select(color =>  Color.From(color)).ToList() ?? new List<Color>(),
            stock:  Stock.From(p.StockQuantity),
            sold: p.Sold,
            isActive: p.IsActive,
            popular: p.Popular,
            createdAt: p.CreatedAt,
            updatedAt: p.UpdatedAt
        );
    }
}