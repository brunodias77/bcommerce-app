using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Orders.Identifiers;

public sealed class OrderItemID : Identifier
{
    private readonly Guid _value;

    private OrderItemID(Guid value)
    {
        _value = value;
    }

    public static OrderItemID Generate() => new(Guid.NewGuid());

    public static OrderItemID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is OrderItemID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}