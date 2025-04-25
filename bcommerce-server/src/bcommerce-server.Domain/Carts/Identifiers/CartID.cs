using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Carts.Identifiers;

public sealed class CartID : Identifier
{
    private readonly Guid _value;

    private CartID(Guid value)
    {
        _value = value;
    }

    public static CartID Generate() => new(Guid.NewGuid());

    public static CartID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is CartID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}