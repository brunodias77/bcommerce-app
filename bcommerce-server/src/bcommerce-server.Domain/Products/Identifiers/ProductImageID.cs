using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.Identifiers;

public sealed class ProductImageID : Identifier
{
    private readonly Guid _value;

    private ProductImageID(Guid value)
    {
        _value = value;
    }

    public static ProductImageID Generate() => new(Guid.NewGuid());

    public static ProductImageID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is ProductImageID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}