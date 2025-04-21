using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Utils;

namespace bcommerce_server.Domain.Addresses.Identifiers;

public sealed class AddressID : Identifier
{
    private readonly Guid _value;

    private AddressID(Guid value)
    {
        _value = value;
    }

    /// <summary>
    /// Gera um novo AddressID com UUID único.
    /// </summary>
    public static AddressID Generate() => new(IdUtils.Uuid());

    /// <summary>
    /// Cria um AddressID a partir de um valor existente.
    /// </summary>
    public static AddressID From(Guid id) => new(id);

    /// <summary>
    /// Retorna o valor interno do identificador.
    /// </summary>
    public override Guid Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not AddressID other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
}