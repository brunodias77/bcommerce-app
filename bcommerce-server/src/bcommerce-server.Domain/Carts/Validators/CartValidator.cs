using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Carts.Validators;

public class CartValidator : Validator
{
    private readonly Cart _cart;

    public CartValidator(Cart cart, IValidationHandler handler)
        : base(handler)
    {
        _cart = cart ?? throw new ArgumentNullException(nameof(cart));
    }

    public override void Validate()
    {
        ValidateCustomerId();
        ValidateItems();
    }

    private void ValidateCustomerId()
    {
        if (_cart.CustomerId == Guid.Empty)
        {
            AddError("'customerId' não pode ser vazio.");
        }
    }

    private void ValidateItems()
    {
        var items = _cart.Items;
        if (items == null)
        {
            AddError("'itens' não podem ser nulos.");
            return;
        }

        int index = 0;
        foreach (var item in items)
        {
            if (item == null)
            {
                AddError($"'item[{index}]' não pode ser nulo.");
                index++;
                continue;
            }

            item.Validate(ValidationHandler); // delega para CartItemValidator
            index++;
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}