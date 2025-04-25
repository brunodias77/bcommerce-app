using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Carts.Identifiers;


public sealed class CartItemID : Identifier
{
    private readonly Guid _value;

    private CartItemID(Guid value)
    {
        _value = value;
    }

    public static CartItemID Generate() => new(Guid.NewGuid());

    public static CartItemID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is CartItemID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}