using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.SeedWork;

/// <summary>
/// Representa uma entidade genérica com um identificador único.
/// </summary>
/// <typeparam name="ID">Tipo do identificador da entidade, que deve herdar de Identifier.</typeparam>
public abstract class Entity<ID> where ID : Identifier
{
    protected readonly ID _id;

    protected Entity(ID id)
    {
        _id = id ?? throw new ArgumentNullException(nameof(id), "'id' não deve ser nulo!");
    }

    /// <summary>
    /// Identificador da entidade.
    /// </summary>
    public ID Id => _id;

    /// <summary>
    /// Método abstrato para validar a entidade usando um handler de validações.
    /// </summary>
    public abstract void Validate(IValidationHandler handler);

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        var other = (Entity<ID>)obj;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();
}


