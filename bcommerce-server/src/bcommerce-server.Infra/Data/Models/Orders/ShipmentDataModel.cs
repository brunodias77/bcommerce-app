namespace bcommerce_server.Infra.Data.Models.Orders;

public sealed record ShipmentDataModel(
    Guid Id,
    Guid OrderId,
    string Carrier,
    string TrackingNumber,
    string Status,
    DateTime? ShippedAt,
    DateTime? DeliveredAt,
    DateTime CreatedAt
);