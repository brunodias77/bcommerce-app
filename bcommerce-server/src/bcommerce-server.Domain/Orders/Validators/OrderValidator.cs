
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Orders.Validators;

public class OrderValidator : Validator
{
    private readonly Order _order;

    public OrderValidator(Order order, IValidationHandler handler)
        : base(handler)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
    }

    public override void Validate()
    {
        ValidateCustomerId();
        ValidateShippingAddressId();
        ValidateStatus();
        ValidateTotalAmount();
        ValidateItems();
        ValidatePayment();
        ValidateShipment();
    }

    private void ValidateCustomerId()
    {
        if (_order.CustomerId == Guid.Empty)
        {
            AddError("'customerId' não pode ser vazio.");
        }
    }

    private void ValidateShippingAddressId()
    {
        if (_order.ShippingAddressId == Guid.Empty)
        {
            AddError("'shippingAddressId' não pode ser vazio.");
        }
    }

    private void ValidateStatus()
    {
        if (_order.Status is null)
        {
            AddError("'status' não pode ser nulo.");
        }
    }

    private void ValidateTotalAmount()
    {
        if (_order.TotalAmount <= 0)
        {
            AddError("'totalAmount' deve ser maior que 0.");
        }
    }

    private void ValidateItems()
    {
        var items = _order.Items;

        if (items is null || !items.Any())
        {
            AddError("O pedido deve conter pelo menos um item.");
            return;
        }

        int index = 0;
        foreach (var item in items)
        {
            if (item is null)
            {
                AddError($"'items[{index}]' não pode ser nulo.");
            }
            else
            {
                item.Validate(ValidationHandler);
            }

            index++;
        }
    }

    private void ValidatePayment()
    {
        _order.Payment?.Validate(ValidationHandler);
    }

    private void ValidateShipment()
    {
        _order.Shipment?.Validate(ValidationHandler);
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}
