using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Entities;

public class Color : AggregateRoot<ColorID>
{
    private string _name;
    private string _value;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    private Color(
        ColorID id,
        string name,
        string value,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _name = name;
        _value = value;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    // ✅ Fábrica para nova cor
    public static Color NewColor(string name, string value)
    {
        var now = DateTime.UtcNow;
        return new Color(ColorID.Generate(), name, value, now, now);
    }

    // ✅ Reconstituição de cor existente
    public static Color With(
        ColorID id,
        string name,
        string value,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new Color(id, name, value, createdAt, updatedAt);
    }

    // ✅ Atualizar dados da cor
    public Color Update(string name, string value)
    {
        _name = name;
        _value = value;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new ColorValidator(this, handler).Validate();
    }

    // ✅ Propriedades públicas
    public string Name => _name;
    public string Value => _value;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;

    public object Clone()
    {
        return With(
            Id,
            Name,
            Value,
            CreatedAt,
            UpdatedAt
        );
    }
}