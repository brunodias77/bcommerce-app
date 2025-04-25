using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Entities;

public class ProductReview : Entity<ProductReviewID>
{
    private int _rating;
    private string? _comment;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    private ProductReview(ProductReviewID id, int rating, string? comment, DateTime createdAt, DateTime updatedAt)
        : base(id)
    {
        _rating = rating;
        _comment = comment;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public static ProductReview Create(int rating, string? comment)
    {
        var now = DateTime.UtcNow;
        return new ProductReview(ProductReviewID.Generate(), rating, comment, now, now);
    }

    public static ProductReview With(ProductReviewID id, int rating, string? comment, DateTime createdAt, DateTime updatedAt)
    {
        return new ProductReview(id, rating, comment, createdAt, updatedAt);
    }

    public ProductReview Update(int rating, string? comment)
    {
        _rating = rating;
        _comment = comment;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new ProductReviewValidator(this, handler).Validate();
    }

    public int Rating => _rating;
    public string? Comment => _comment;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;
}