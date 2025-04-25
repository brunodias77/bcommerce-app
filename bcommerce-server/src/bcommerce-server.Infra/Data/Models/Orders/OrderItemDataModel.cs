namespace bcommerce_server.Infra.Data.Models.Orders;

public sealed record OrderItemDataModel(
    Guid Id,
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    DateTime CreatedAt
);