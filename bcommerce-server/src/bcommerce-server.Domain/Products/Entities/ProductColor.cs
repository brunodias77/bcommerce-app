using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.Validators;
using bcommerce_server.Domain.Products.ValueObjects;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Entities;

public class ProductColor : Entity<ProductColorID>
{
    private ColorValue _color;
    private DateTime _createdAt;

    private ProductColor(ProductColorID id, ColorValue color, DateTime createdAt)
        : base(id)
    {
        _color = color;
        _createdAt = createdAt;
    }

    public static ProductColor Create(ColorValue color)
    {
        return new ProductColor(ProductColorID.Generate(), color, DateTime.UtcNow);
    }

    public static ProductColor With(ProductColorID id, ColorValue color, DateTime createdAt)
    {
        return new ProductColor(id, color, createdAt);
    }

    public override void Validate(IValidationHandler handler)
    {
        new ProductColorValidator(this, handler).Validate();
    }

    public ColorValue Color => _color;
    public DateTime CreatedAt => _createdAt;
}