using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.Identifiers;

public sealed class ProductColorID : Identifier
{
    private readonly Guid _value;

    private ProductColorID(Guid value)
    {
        _value = value;
    }

    public static ProductColorID Generate() => new(Guid.NewGuid());

    public static ProductColorID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is ProductColorID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}