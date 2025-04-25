using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.ValueObjects;

public sealed class ColorValue : ValueObject
{
    public string Value { get; }

    private ColorValue(string value)
    {
        Value = value.Trim();
    }

    public static ColorValue From(string raw) => new(raw);

    public override string ToString() => Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}