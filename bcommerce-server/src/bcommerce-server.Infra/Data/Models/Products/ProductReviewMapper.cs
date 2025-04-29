using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Products.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Products;

public static class ProductReviewMapper
{
    public static ProductReview ToDomain(ProductReviewDataModel model)
    {
        return ProductReview.With(
            ProductReviewID.From(model.Id),
            model.Rating,
            model.Comment,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public static ProductReviewDataModel ToDataModel(ProductReview review, Guid productId, Guid customerId)
    {
        return new ProductReviewDataModel(
            review.Id.Value,
            productId,         // <- novo parâmetro
            customerId,        // <- novo parâmetro
            review.Rating,
            review.Comment,
            review.CreatedAt,
            review.UpdatedAt
        );
    }
}