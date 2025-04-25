using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.ValueObjects;

namespace bcommerce_server.Infra.Data.Models.Products;


public static class ProductMapper
{
    public static Product ToDomain(ProductDataModel model, List<ProductImage> images, List<ProductColor> colors, List<ProductReview> reviews)
    {
        return Product.With(
            ProductID.From(model.Id),
            model.Name,
            model.Description,
            model.Price,
            model.OldPrice,
            model.CategoryId,
            model.StockQuantity,
            model.Sold,
            model.IsActive,
            model.Popular,
            images,
            colors,
            reviews,
            model.DeletedAt,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public static ProductDataModel ToDataModel(Product product)
    {
        return new ProductDataModel(
            product.Id.Value,
            product.Name,
            product.Description,
            product.Price,
            product.OldPrice,
            product.CategoryId,
            product.StockQuantity,
            product.Sold,
            product.IsActive,
            product.Popular,
            product.DeletedAt,
            product.CreatedAt,
            product.UpdatedAt
        );
    }
}