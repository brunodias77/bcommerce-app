using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Entities;

public sealed class ProductColor : Entity<ProductColorID>
{
    private readonly Guid _productId;
    private readonly Color _color;
    private readonly DateTime _createdAt;
    private DateTime _updatedAt;

    private ProductColor(
        ProductColorID id,
        Guid productId,
        Color color,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _productId = productId;
        _color = color ?? throw new ArgumentNullException(nameof(color));
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public static ProductColor Create(Guid productId, Color color)
    {
        var now = DateTime.UtcNow;
        return new ProductColor(ProductColorID.Generate(), productId, color, now, now);
    }

    public static ProductColor With(
        ProductColorID id,
        Guid productId,
        Color color,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new ProductColor(id, productId, color, createdAt, updatedAt);
    }

    public ProductColor UpdateTimestamp()
    {
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new ProductColorValidator(this, handler).Validate();
    }

    // Propriedades públicas
    public Guid ProductId => _productId;
    public Color Color => _color;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;
}