using System;
using System.Text.RegularExpressions;
using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Validators;

public sealed class ProductColorValidator : Validator
{
    private readonly ProductColor _productColor;

    public ProductColorValidator(ProductColor productColor, IValidationHandler handler)
        : base(handler)
    {
        _productColor = productColor ?? throw new ArgumentNullException(nameof(productColor));
    }

    public override void Validate()
    {
        ValidateProductId();
        ValidateColor();
    }

    private void ValidateProductId()
    {
        if (_productColor.ProductId == Guid.Empty)
            AddError("O 'ProductId' não pode ser vazio.");
    }

    private void ValidateColor()
    {
        var color = _productColor.Color;

        if (color == null)
        {
            AddError("A instância da cor não pode ser nula.");
            return;
        }

        if (color.Id.Value == Guid.Empty)
            AddError("O 'ColorId' não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(color.Name))
            AddError("O nome da cor não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(color.Value))
        {
            AddError("O valor da cor não pode ser vazio.");
            return;
        }

        var isHex = Regex.IsMatch(color.Value, @"^#([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$");
        var isNamed = Regex.IsMatch(color.Value, @"^[A-Za-z]+$");

        if (!isHex && !isNamed)
            AddError("A cor deve ser um valor HEX válido (ex: #FF00FF) ou um nome (ex: Red).");
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}