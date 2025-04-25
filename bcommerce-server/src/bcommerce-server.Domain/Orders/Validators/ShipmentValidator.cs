using bcommerce_server.Domain.Orders.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Orders.Validators;

public class ShipmentValidator : Validator
{
    private readonly Shipment _shipment;

    public ShipmentValidator(Shipment shipment, IValidationHandler handler)
        : base(handler)
    {
        _shipment = shipment ?? throw new ArgumentNullException(nameof(shipment));
    }

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(_shipment.Carrier))
        {
            AddError("'carrier' não pode estar em branco.");
        }

        if (string.IsNullOrWhiteSpace(_shipment.TrackingNumber))
        {
            AddError("'trackingNumber' não pode estar em branco.");
        }

        if (_shipment.Status is null)
        {
            AddError("'status' do envio não pode ser nulo.");
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}
