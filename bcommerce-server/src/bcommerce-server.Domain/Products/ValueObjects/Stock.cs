using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.ValueObjects;

/// <summary>
/// Representa o estoque de um produto.
/// </summary>
public sealed class Stock : ValueObject
{
    public int Quantity { get; }

    private Stock(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Estoque não pode ser negativo.");

        Quantity = quantity;
    }

    public static Stock From(int quantity) => new(quantity);

    public Stock Increase(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Não é possível adicionar uma quantidade negativa ao estoque.");

        return new Stock(Quantity + amount);
    }

    public Stock Decrease(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Não é possível remover uma quantidade negativa do estoque.");

        if (Quantity - amount < 0)
            throw new InvalidOperationException("Estoque insuficiente para a operação solicitada.");

        return new Stock(Quantity - amount);
    }

    public bool IsOutOfStock() => Quantity <= 0;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Quantity;
    }

    public override string ToString() => Quantity.ToString();
}