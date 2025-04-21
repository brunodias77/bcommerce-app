using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Utils;

namespace bcommerce_server.Domain.Customers.Identifiers;


/// <summary>
/// Identificador único para a entidade Customer.
/// </summary>
public sealed class CustomerID : Identifier
{
    private readonly Guid _value;

    private CustomerID(Guid value)
    {
        _value = value;
    }

    /// <summary>
    /// Cria uma nova instância com GUID aleatório.
    /// </summary>
    public static CustomerID Generate() => new(Guid.NewGuid());

    /// <summary>
    /// Cria uma nova instância a partir de um Guid existente.
    /// </summary>
    public static CustomerID From(Guid id) => new(id);

    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj)
    {
        return obj is CustomerID other && Value.Equals(other.Value);
    }

    public override int GetHashCode() => Value.GetHashCode();
}