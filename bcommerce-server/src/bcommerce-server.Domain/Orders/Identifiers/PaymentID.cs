using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Orders.Identifiers;

public sealed class PaymentID : Identifier
{
    private readonly Guid _value;

    private PaymentID(Guid value)
    {
        _value = value;
    }

    public static PaymentID Generate() => new(Guid.NewGuid());

    public static PaymentID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is PaymentID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}
