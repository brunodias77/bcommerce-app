
using System.Text.RegularExpressions;
using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Validators;

public class ColorValidator : Validator
{
    private readonly Color _color;

    public ColorValidator(Color color, IValidationHandler handler)
        : base(handler)
    {
        _color = color ?? throw new ArgumentNullException(nameof(color));
    }

    public override void Validate()
    {
        ValidateName();
        ValidateValue();
    }

    private void ValidateName()
    {
        if (string.IsNullOrWhiteSpace(_color.Name))
        {
            AddError("O nome da cor não pode ser vazio.");
            return;
        }

        if (_color.Name.Length > 50)
        {
            AddError("O nome da cor deve ter no máximo 50 caracteres.");
        }
    }

    private void ValidateValue()
    {
        var value = _color.Value;

        if (string.IsNullOrWhiteSpace(value))
        {
            AddError("O valor da cor não pode ser vazio.");
            return;
        }

        var isHex = Regex.IsMatch(value, @"^#([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$");
        var isNamed = Regex.IsMatch(value, @"^[A-Za-z]+$");

        if (!isHex && !isNamed)
        {
            AddError("O valor da cor deve ser um nome (ex: Red) ou um valor hexadecimal válido (ex: #FF00FF).");
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}