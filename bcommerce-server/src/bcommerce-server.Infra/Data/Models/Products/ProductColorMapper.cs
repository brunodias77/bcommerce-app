using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.ValueObjects;

namespace bcommerce_server.Infra.Data.Models.Products;

public static class ProductColorMapper
{
    public static ProductColor ToDomain(ProductColorDataModel model)
    {
        return ProductColor.With(
            ProductColorID.From(model.Id),
            ColorValue.From(model.ColorValue),
            model.CreatedAt
        );
    }

    public static ProductColorDataModel ToDataModel(ProductColor color, Guid productId)
    {
        return new ProductColorDataModel(
            color.Id.Value,
            productId,
            color.Color.Value,
            color.CreatedAt
        );
    }
}