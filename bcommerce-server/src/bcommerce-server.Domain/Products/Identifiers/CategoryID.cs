using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.Identifiers;

public sealed class CategoryID : Identifier
{
    private readonly Guid _value;

    private CategoryID(Guid value)
    {
        _value = value;
    }

    public static CategoryID Generate() => new(Guid.NewGuid());
    public static CategoryID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is CategoryID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}