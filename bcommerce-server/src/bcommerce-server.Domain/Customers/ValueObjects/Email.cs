using System.Text.RegularExpressions;
using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Customers.ValueObjects;

/// <summary>
/// Representa um endereço de e-mail válido no domínio.
/// </summary>
public sealed class Email : ValueObject
{
    public string Address { get; }

    private Email(string address)
    {
        Address = address.ToLowerInvariant();
    }

    public static Email From(string address) => new(address);

    public override string ToString() => Address;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}
