using System.Text.RegularExpressions;
using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Customers.ValueObjects;

/// <summary>
/// Representa um endereço de e-mail válido no domínio.
/// </summary>
public sealed class Email : ValueObject
{
    private static readonly Regex EmailRegex =
        new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Address { get; }

    private Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email não pode ser vazio.");

        if (!EmailRegex.IsMatch(address))
            throw new ArgumentException("Email em formato inválido.");

        Address = address.ToLowerInvariant();
    }

    public static Email From(string address) => new(address);

    public override string ToString() => Address;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}