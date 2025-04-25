using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Coupons.Identifiers;

public sealed class CustomerCouponID : Identifier
{
    private readonly Guid _value;

    private CustomerCouponID(Guid value)
    {
        _value = value;
    }

    public static CustomerCouponID Generate() => new(Guid.NewGuid());

    public static CustomerCouponID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is CustomerCouponID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}