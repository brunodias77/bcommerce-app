using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Utils;

namespace bcommerce_server.Domain.Customers.Identifiers;


public sealed class CustomerID : Identifier
{
    private readonly string _value;

    private CustomerID(string value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Cria uma nova instância com UUID gerado automaticamente.
    /// </summary>
    public static CustomerID Generate() => new(IdUtils.Uuid());

    /// <summary>
    /// Cria uma nova instância a partir de um valor explícito.
    /// </summary>
    public static CustomerID From(string id) => new(id);

    /// <summary>
    /// Valor interno do identificador.
    /// </summary>
    public override string Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not CustomerID other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
}