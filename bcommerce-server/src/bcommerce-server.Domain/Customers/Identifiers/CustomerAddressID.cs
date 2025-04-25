using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Customers.Identifiers;

public sealed class CustomerAddressID : Identifier
{
    private readonly Guid _value;

    private CustomerAddressID(Guid value)
    {
        _value = value;
    }

    public static CustomerAddressID Generate() => new(Guid.NewGuid());

    public static CustomerAddressID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is CustomerAddressID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}