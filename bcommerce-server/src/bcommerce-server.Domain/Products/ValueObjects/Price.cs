using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.ValueObjects;

/// <summary>
/// Representa o valor monetário de um produto.
/// </summary>
public sealed class Price : ValueObject
{
    public decimal Amount { get; }

    private Price(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("O preço não pode ser negativo.");
        Amount = amount;
    }

    public static Price From(decimal amount) => new(amount);

    public override string ToString() => Amount.ToString("F2");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}