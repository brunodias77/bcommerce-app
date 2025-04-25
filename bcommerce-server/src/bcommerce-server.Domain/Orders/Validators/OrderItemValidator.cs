using bcommerce_server.Domain.Orders.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Orders.Validators;

public class OrderItemValidator : Validator
{
    private readonly OrderItem _item;

    public OrderItemValidator(OrderItem item, IValidationHandler handler)
        : base(handler)
    {
        _item = item ?? throw new ArgumentNullException(nameof(item));
    }

    public override void Validate()
    {
        if (_item.ProductId == Guid.Empty)
        {
            AddError("'productId' não pode ser vazio.");
        }

        if (_item.Quantity <= 0)
        {
            AddError("'quantity' deve ser maior que 0.");
        }

        if (_item.UnitPrice <= 0)
        {
            AddError("'unitPrice' deve ser maior que 0.");
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}