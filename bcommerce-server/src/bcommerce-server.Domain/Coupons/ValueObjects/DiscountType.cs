using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Coupons.ValueObjects;

public sealed class DiscountType : ValueObject
{
    public const string Fixed = "fixed";
    public const string Percent = "percent";

    public string Value { get; }

    private DiscountType(string value)
    {
        Value = value.ToLowerInvariant();
    }

    public static DiscountType From(string value)
    {
        var normalized = value.ToLowerInvariant();

        if (normalized != Fixed && normalized != Percent)
            throw new ArgumentException($"Tipo de desconto inválido: {value}. Use '{Fixed}' ou '{Percent}'.");

        return new DiscountType(normalized);
    }

    public bool IsFixed => Value == Fixed;
    public bool IsPercent => Value == Percent;

    public override string ToString() => Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}