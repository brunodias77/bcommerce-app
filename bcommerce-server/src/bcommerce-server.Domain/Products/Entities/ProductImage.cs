using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Entities;

public class ProductImage : Entity<ProductImageID>
{
    private string _url;
    private DateTime _createdAt;

    private ProductImage(ProductImageID id, string url, DateTime createdAt)
        : base(id)
    {
        _url = url;
        _createdAt = createdAt;
    }

    public static ProductImage Create(string url)
    {
        return new ProductImage(ProductImageID.Generate(), url, DateTime.UtcNow);
    }

    public static ProductImage With(ProductImageID id, string url, DateTime createdAt)
    {
        return new ProductImage(id, url, createdAt);
    }

    public override void Validate(IValidationHandler handler)
    {
        new ProductImageValidator(this, handler).Validate();
    }

    public string Url => _url;
    public DateTime CreatedAt => _createdAt;
}