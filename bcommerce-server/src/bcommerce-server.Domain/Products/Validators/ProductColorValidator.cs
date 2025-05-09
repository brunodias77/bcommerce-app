using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Validators;

public class ProductColorValidator : Validator
{
    private readonly ProductColor _color;

    public ProductColorValidator(ProductColor color, IValidationHandler handler)
        : base(handler)
    {
        _color = color ?? throw new ArgumentNullException(nameof(color));
    }

    public override void Validate()
    {
        var value = _color.Color.Value;

        if (string.IsNullOrWhiteSpace(value))
        {
            AddError("'color' não pode estar em branco.");
            return;
        }

        if (value.Length > 20)
        {
            AddError("'color' deve ter no máximo 20 caracteres.");
        }

        // A validação de formato (hex, nome de cor) pode ficar no ProductValidator, como combinamos
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}