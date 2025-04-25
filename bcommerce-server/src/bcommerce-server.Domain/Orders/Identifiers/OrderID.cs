using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Orders.Identifiers;

public sealed class OrderID : Identifier
{
    private readonly Guid _value;

    private OrderID(Guid value)
    {
        _value = value;
    }

    /// <summary>
    /// Gera um novo identificador.
    /// </summary>
    public static OrderID Generate() => new(Guid.NewGuid());

    /// <summary>
    /// Constrói a partir de um Guid existente.
    /// </summary>
    public static OrderID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj) =>
        obj is OrderID other && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();
}