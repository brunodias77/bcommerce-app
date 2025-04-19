using bcommerce_server.Domain.Categories;

namespace bcommerce_server.Domain.Validations.Handlers;

/// <summary>
/// Validador para entidades do tipo Category.
/// </summary>
public class CategoryValidator : Validator
{
    private const int NAME_MAX_LENGTH = 255;
    private const int NAME_MIN_LENGTH = 3;

    private readonly Category _category;

    public CategoryValidator(Category category, IValidationHandler handler)
        : base(handler)
    {
        _category = category;
    }

    public override void Validate()
    {
        ValidateNameRestrictions();
    }

    private void ValidateNameRestrictions()
    {
        var name = _category.Name;

        ValidateNameNotNull(name);
        if (name is null) return;

        ValidateNameNotEmpty(name);
        ValidateNameSize(name);
    }

    private void ValidateNameNotNull(string? name)
    {
        if (name is null)
            AddError("'name' should not be null");
    }

    private void ValidateNameNotEmpty(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            AddError("'name' should not be empty");
    }

    private void ValidateNameSize(string name)
    {
        var length = name.Trim().Length;
        if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
        {
            AddError($"'name' must be between {NAME_MIN_LENGTH} and {NAME_MAX_LENGTH} characters");
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}