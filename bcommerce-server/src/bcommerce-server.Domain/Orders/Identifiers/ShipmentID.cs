using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Orders.Identifiers;

public sealed class ShipmentID : Identifier
{
    private readonly Guid _value;

    private ShipmentID(Guid value)
    {
        _value = value;
    }

    public static ShipmentID Generate() => new(Guid.NewGuid());

    public static ShipmentID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is ShipmentID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}