using bcommerce_server.Domain.Orders.Identifiers;
using bcommerce_server.Domain.Orders.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Orders.Entities;

public class OrderItem : Entity<OrderItemID>
{
    private Guid _productId;
    private int _quantity;
    private decimal _unitPrice;
    private DateTime _createdAt;

    private OrderItem(OrderItemID id, Guid productId, int quantity, decimal unitPrice, DateTime createdAt)
        : base(id)
    {
        _productId = productId;
        _quantity = quantity;
        _unitPrice = unitPrice;
        _createdAt = createdAt;
    }

    public static OrderItem Create(Guid productId, int quantity, decimal unitPrice)
    {
        return new OrderItem(OrderItemID.Generate(), productId, quantity, unitPrice, DateTime.UtcNow);
    }

    public static OrderItem With(OrderItemID id, Guid productId, int quantity, decimal unitPrice, DateTime createdAt)
    {
        return new OrderItem(id, productId, quantity, unitPrice, createdAt);
    }

    public override void Validate(IValidationHandler handler)
    {
        new OrderItemValidator(this, handler).Validate();
    }

    public Guid ProductId => _productId;
    public int Quantity => _quantity;
    public decimal UnitPrice => _unitPrice;
    public DateTime CreatedAt => _createdAt;
}