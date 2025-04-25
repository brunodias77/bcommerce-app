using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products;

public class Category : AggregateRoot<CategoryID>
{
    private string _name;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    private Category(CategoryID id, string name, DateTime createdAt, DateTime updatedAt)
        : base(id)
    {
        _name = name;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public static Category Create(string name)
    {
        var now = DateTime.UtcNow;
        return new Category(CategoryID.Generate(), name, now, now);
    }

    public static Category With(CategoryID id, string name, DateTime createdAt, DateTime updatedAt)
    {
        return new Category(id, name, createdAt, updatedAt);
    }

    public Category Update(string name)
    {
        _name = name;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new CategoryValidator(this, handler).Validate();
    }

    public string Name => _name;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;
}
