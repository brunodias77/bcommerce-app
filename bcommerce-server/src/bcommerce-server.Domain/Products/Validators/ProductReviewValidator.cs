using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Validators;

public class ProductReviewValidator : Validator
{
    private readonly ProductReview _review;

    public ProductReviewValidator(ProductReview review, IValidationHandler handler)
        : base(handler)
    {
        _review = review ?? throw new ArgumentNullException(nameof(review));
    }

    public override void Validate()
    {
        if (_review.Rating < 1 || _review.Rating > 5)
        {
            AddError("'rating' deve estar entre 1 e 5.");
        }

        if (_review.Comment?.Length > 1000)
        {
            AddError("'comment' não pode ter mais que 1000 caracteres.");
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}
