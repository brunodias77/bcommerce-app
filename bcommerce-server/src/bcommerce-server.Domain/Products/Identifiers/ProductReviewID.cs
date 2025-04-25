using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.Identifiers;

public sealed class ProductReviewID : Identifier
{
    private readonly Guid _value;

    private ProductReviewID(Guid value)
    {
        _value = value;
    }

    public static ProductReviewID Generate() => new(Guid.NewGuid());

    public static ProductReviewID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is ProductReviewID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}