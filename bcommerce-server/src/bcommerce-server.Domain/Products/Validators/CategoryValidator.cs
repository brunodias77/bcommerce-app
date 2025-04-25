using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Validators;

public class CategoryValidator : Validator
{
    private const int NAME_MIN_LENGTH = 1;
    private const int NAME_MAX_LENGTH = 100;

    private readonly Category _category;

    public CategoryValidator(Category category, IValidationHandler handler)
        : base(handler)
    {
        _category = category ?? throw new ArgumentNullException(nameof(category));
    }

    public override void Validate()
    {
        ValidateName();
    }

    private void ValidateName()
    {
        var name = _category.Name;

        if (string.IsNullOrWhiteSpace(name))
        {
            AddError("'nome da categoria' não pode estar em branco.");
            return;
        }

        var length = name.Trim().Length;
        if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
        {
            AddError($"'nome da categoria' deve ter entre {NAME_MIN_LENGTH} e {NAME_MAX_LENGTH} caracteres.");
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}