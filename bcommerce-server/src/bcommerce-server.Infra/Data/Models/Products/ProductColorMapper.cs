using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Products.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Products;

public static class ProductColorMapper
{
    public static ProductColor ToDomain(ProductColorDataModel model)
    {
        var color = Color.With(
            ColorID.From(model.ColorId),
            model.ColorName,
            model.ColorValue,
            model.CreatedAt,
            model.UpdatedAt
        );

        return ProductColor.With(
            ProductColorID.From(model.Id),
            model.ProductId,
            color,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public static ProductColorDataModel ToDataModel(ProductColor entity)
    {
        return new ProductColorDataModel(
            Id: entity.Id.Value,
            ProductId: entity.ProductId,
            ColorId: entity.Color.Id.Value,
            ColorName: entity.Color.Name,
            ColorValue: entity.Color.Value,
            CreatedAt: entity.CreatedAt,
            UpdatedAt: entity.UpdatedAt
        );
    }
}