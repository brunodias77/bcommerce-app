using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.ValueObjects;

/// <summary>
/// Representa a categoria de um produto.
/// </summary>
public sealed class Category : ValueObject
{
    public string Name { get; }

    private Category(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Categoria não pode ser vazia.");

        Name = name.Trim();
    }

    public static Category From(string name) => new(name);

    public override string ToString() => Name;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name.ToLowerInvariant();
    }
}