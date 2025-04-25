using bcommerce_server.Domain.Common;
using bcommerce_server.Domain.Orders.Identifiers;
using bcommerce_server.Domain.Orders.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Orders.Entities;

public class Shipment : Entity<ShipmentID>
{
    private string _carrier;
    private string _trackingNumber;
    private Status _status;
    private DateTime? _shippedAt;
    private DateTime? _deliveredAt;
    private DateTime _createdAt;

    private Shipment(ShipmentID id, string carrier, string trackingNumber, Status status, DateTime? shippedAt, DateTime? deliveredAt, DateTime createdAt)
        : base(id)
    {
        _carrier = carrier;
        _trackingNumber = trackingNumber;
        _status = status;
        _shippedAt = shippedAt;
        _deliveredAt = deliveredAt;
        _createdAt = createdAt;
    }

    public static Shipment Create(string carrier, string trackingNumber, Status status)
    {
        return new Shipment(ShipmentID.Generate(), carrier, trackingNumber, status, null, null, DateTime.UtcNow);
    }

    public static Shipment With(ShipmentID id, string carrier, string trackingNumber, Status status, DateTime? shippedAt, DateTime? deliveredAt, DateTime createdAt)
    {
        return new Shipment(id, carrier, trackingNumber, status, shippedAt, deliveredAt, createdAt);
    }

    public override void Validate(IValidationHandler handler)
    {
        new ShipmentValidator(this, handler).Validate();
    }

    public string Carrier => _carrier;
    public string TrackingNumber => _trackingNumber;
    public Status Status => _status;
    public DateTime? ShippedAt => _shippedAt;
    public DateTime? DeliveredAt => _deliveredAt;
    public DateTime CreatedAt => _createdAt;
}