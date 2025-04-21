namespace bcommerce_server.Domain.SeedWork;

/// <summary>
/// Representa um identificador único para entidades no domínio.
/// </summary>
public abstract class Identifier : ValueObject
{
    public abstract Guid Value { get; }

    public override string ToString() => Value.ToString();
}