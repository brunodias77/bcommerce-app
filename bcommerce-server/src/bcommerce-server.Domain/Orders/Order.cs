using bcommerce_server.Domain.Common;
using bcommerce_server.Domain.Orders.Entities;
using bcommerce_server.Domain.Orders.Identifiers;
using bcommerce_server.Domain.Orders.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Orders;

public class Order : AggregateRoot<OrderID>
{
    private Guid _customerId;
    private Guid _shippingAddressId;
    private Guid? _couponId;
    private Status _status;
    private decimal _totalAmount;
    private List<OrderItem> _items;
    private Payment? _payment;
    private Shipment? _shipment;
    private DateTime _placedAt;
    private DateTime? _shippedAt;
    private DateTime? _deliveredAt;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    private Order(
        OrderID id,
        Guid customerId,
        Guid shippingAddressId,
        Guid? couponId,
        Status status,
        decimal totalAmount,
        List<OrderItem> items,
        Payment? payment,
        Shipment? shipment,
        DateTime placedAt,
        DateTime? shippedAt,
        DateTime? deliveredAt,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _customerId = customerId;
        _shippingAddressId = shippingAddressId;
        _couponId = couponId;
        _status = status;
        _totalAmount = totalAmount;
        _items = items ?? new();
        _payment = payment;
        _shipment = shipment;
        _placedAt = placedAt;
        _shippedAt = shippedAt;
        _deliveredAt = deliveredAt;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public static Order Create(
        Guid customerId,
        Guid shippingAddressId,
        Status status,
        decimal totalAmount,
        List<OrderItem> items,
        Guid? couponId = null
    )
    {
        var now = DateTime.UtcNow;

        return new Order(
            OrderID.Generate(),
            customerId,
            shippingAddressId,
            couponId,
            status,
            totalAmount,
            items,
            null,
            null,
            now,
            null,
            null,
            now,
            now
        );
    }

    public static Order With(
        OrderID id,
        Guid customerId,
        Guid shippingAddressId,
        Guid? couponId,
        Status status,
        decimal totalAmount,
        List<OrderItem> items,
        Payment? payment,
        Shipment? shipment,
        DateTime placedAt,
        DateTime? shippedAt,
        DateTime? deliveredAt,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new Order(id, customerId, shippingAddressId, couponId, status, totalAmount, items, payment, shipment, placedAt, shippedAt, deliveredAt, createdAt, updatedAt);
    }

    public Order AddItem(OrderItem item)
    {
        _items.Add(item);
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Order SetPayment(Payment payment)
    {
        _payment = payment;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Order SetShipment(Shipment shipment)
    {
        _shipment = shipment;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Order UpdateStatus(Status status)
    {
        _status = status;
        _updatedAt = DateTime.UtcNow;

        if (status.Value == Status.Shipped)
            _shippedAt = DateTime.UtcNow;

        if (status.Value == Status.Delivered)
            _deliveredAt = DateTime.UtcNow;

        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new OrderValidator(this, handler).Validate();
    }

    // 📖 Getters públicos (read-only)
    public Guid CustomerId => _customerId;
    public Guid ShippingAddressId => _shippingAddressId;
    public Guid? CouponId => _couponId;
    public Status Status => _status;
    public decimal TotalAmount => _totalAmount;
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public Payment? Payment => _payment;
    public Shipment? Shipment => _shipment;
    public DateTime PlacedAt => _placedAt;
    public DateTime? ShippedAt => _shippedAt;
    public DateTime? DeliveredAt => _deliveredAt;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;

    public object Clone()
    {
        return With(
            Id,
            CustomerId,
            ShippingAddressId,
            CouponId,
            Status,
            TotalAmount,
            _items.Select(i =>
                OrderItem.With(i.Id, i.ProductId, i.Quantity, i.UnitPrice, i.CreatedAt)
            ).ToList(),
            _payment,
            _shipment,
            PlacedAt,
            ShippedAt,
            DeliveredAt,
            CreatedAt,
            UpdatedAt
        );
    }
}