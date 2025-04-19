namespace bcommerce_server.Domain.SeedWork;

/// <summary>
/// Representa um objeto de valor no modelo de domínio.
/// </summary>
public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;

        return GetEqualityComponents().SequenceEqual(
            ((ValueObject)obj).GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (hash, obj) => hash * 23 + (obj?.GetHashCode() ?? 0));
    }}