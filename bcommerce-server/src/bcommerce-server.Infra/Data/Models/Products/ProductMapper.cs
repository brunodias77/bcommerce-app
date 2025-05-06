using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Infra.Data.Models.Products;

public static class ProductMapper
{
    public static Product ToDomain(
        ProductDataModel model,
        List<ProductImage> images,
        List<ProductColor> colors,
        List<ProductReview> reviews,
        Category? category = null // ✅ novo parâmetro opcional
    )
    {
        return Product.With(
            ProductID.From(model.Id),
            model.Name,
            model.Description,
            model.Price,
            model.OldPrice,
            CategoryID.From(model.CategoryId),
            model.StockQuantity,
            model.Sold,
            model.IsActive,
            model.Popular,
            images,
            colors,
            reviews,
            model.DeletedAt,
            model.CreatedAt,
            model.UpdatedAt,
            category // ✅ agora passamos a entidade Category se disponível
        );
    }

    public static ProductDataModel ToDataModel(Product product)
    {
        return new ProductDataModel
        {
            Id = product.Id.Value,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            OldPrice = product.OldPrice,
            CategoryId = product.CategoryId.Value,
            StockQuantity = product.StockQuantity,
            Sold = product.Sold,
            IsActive = product.IsActive,
            Popular = product.Popular,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            DeletedAt = product.DeletedAt
        };
    }
}

// using bcommerce_server.Domain.Products;
// using bcommerce_server.Domain.Products.Entities;
// using bcommerce_server.Domain.Products.Identifiers;
// using bcommerce_server.Domain.Products.ValueObjects;
//
// namespace bcommerce_server.Infra.Data.Models.Products;
//
//
// public static class ProductMapper
// {
//     public static Product ToDomain(ProductDataModel model, List<ProductImage> images, List<ProductColor> colors, List<ProductReview> reviews)
//     {
//         return Product.With(
//             ProductID.From(model.Id),
//             model.Name,
//             model.Description,
//             model.Price,
//             model.OldPrice,
//             CategoryID.From(model.CategoryId), // <-- Transforma Guid em CategoryID VO
//             model.StockQuantity,
//             model.Sold,
//             model.IsActive,
//             model.Popular,
//             images,
//             colors,
//             reviews,
//             model.DeletedAt,
//             model.CreatedAt,
//             model.UpdatedAt
//         );
//     }
//
//     public static ProductDataModel ToDataModel(Product product)
//     {
//         return new ProductDataModel
//         {
//             Id = product.Id.Value,
//             Name = product.Name,
//             Description = product.Description,
//             Price = product.Price,
//             OldPrice = product.OldPrice,
//             CategoryId = product.CategoryId.Value, // <-- CategoryID VO para Guid
//             StockQuantity = product.StockQuantity,
//             Sold = product.Sold,
//             IsActive = product.IsActive,
//             Popular = product.Popular,
//             CreatedAt = product.CreatedAt,
//             UpdatedAt = product.UpdatedAt,
//             DeletedAt = product.DeletedAt
//         };
//     }
// }
