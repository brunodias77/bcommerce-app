using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Utils;

namespace bcommerce_server.Domain.Categories;

/// <summary>
/// Representa o identificador único da entidade Category.
/// Encapsula o valor do ID como uma string e garante imutabilidade.
/// </summary>
public sealed class CategoryID : Identifier
{
    private readonly string _value;

    private CategoryID(string value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Cria uma nova instância de CategoryID com UUID gerado automaticamente.
    /// </summary>
    public static CategoryID Unique() => new(IdUtils.Uuid());

    /// <summary>
    /// Cria uma nova instância de CategoryID a partir de um valor específico.
    /// </summary>
    public static CategoryID From(string id) => new(id);

    /// <summary>
    /// Cria um novo ID de categoria com UUID gerado automaticamente.
    /// </summary>
    public static CategoryID Generate() => new(IdUtils.Uuid());

    /// <summary>
    /// Retorna o valor do identificador.
    /// </summary>
    public override string Value => _value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // TODO
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not CategoryID other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
}