namespace bcommerce_server.Infra.Data.Models.Orders;

public sealed record OrderDataModel(
    Guid Id,
    Guid CustomerId,
    Guid ShippingAddressId,
    Guid? CouponId,
    string Status,
    decimal TotalAmount,
    DateTime PlacedAt,
    DateTime? ShippedAt,
    DateTime? DeliveredAt,
    DateTime CreatedAt,
    DateTime UpdatedAt
);