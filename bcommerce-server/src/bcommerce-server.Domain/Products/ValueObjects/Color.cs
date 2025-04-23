using System.Text.RegularExpressions;
using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.ValueObjects;

/// <summary>
/// Representa uma cor de produto (nome ou valor HEX).
/// </summary>
public sealed class Color : ValueObject
{
    public string Value { get; }

    private Color(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Cor não pode ser vazia.");

        // Aceita valores hexadecimais ou nomes simples
        var isHex = Regex.IsMatch(value, @"^#(?:[0-9a-fA-F]{3}){1,2}$");
        var isNamedColor = Regex.IsMatch(value, @"^[a-zA-Z]+$");

        if (!isHex && !isNamedColor)
            throw new ArgumentException("Formato de cor inválido.");

        Value = value;
    }

    public static Color From(string value) => new(value);

    public override string ToString() => Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}