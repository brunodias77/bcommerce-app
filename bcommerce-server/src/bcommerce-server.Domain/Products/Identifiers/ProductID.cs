using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.Identifiers;

/// <summary>
/// Identificador único para a entidade Product.
/// </summary>
public sealed class ProductID : Identifier
{
    private readonly Guid _value;

    private ProductID(Guid value)
    {
        _value = value;
    }

    /// <summary>
    /// Cria uma nova instância com GUID aleatório.
    /// </summary>
    public static ProductID Generate() => new(Guid.NewGuid());

    /// <summary>
    /// Cria uma nova instância a partir de um Guid existente.
    /// </summary>
    public static ProductID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj)
    {
        return obj is ProductID other && Value.Equals(other.Value);
    }

    public override int GetHashCode() => Value.GetHashCode();
}