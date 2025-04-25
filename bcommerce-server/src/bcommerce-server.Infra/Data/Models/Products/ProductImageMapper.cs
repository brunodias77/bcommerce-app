using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Products.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Products;

public static class ProductImageMapper
{
    public static ProductImage ToDomain(ProductImageDataModel model)
    {
        return ProductImage.With(
            ProductImageID.From(model.Id),
            model.Url,
            model.CreatedAt
        );
    }

    public static ProductImageDataModel ToDataModel(ProductImage image, Guid productId)
    {
        return new ProductImageDataModel(
            image.Id.Value,
            productId,
            image.Url,
            image.CreatedAt
        );
    }
}