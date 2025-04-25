using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Validators;

public class ProductImageValidator : Validator
{
    private readonly ProductImage _image;

    public ProductImageValidator(ProductImage image, IValidationHandler handler)
        : base(handler)
    {
        _image = image ?? throw new ArgumentNullException(nameof(image));
    }

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(_image.Url))
        {
            AddError("'url' da imagem não pode estar em branco.");
        }

        if (_image.Url.Length > 2048)
        {
            AddError("'url' da imagem não pode ter mais que 2048 caracteres.");
        }

        // Pode adicionar validações específicas de formato se quiser (regex para URL, etc)
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}