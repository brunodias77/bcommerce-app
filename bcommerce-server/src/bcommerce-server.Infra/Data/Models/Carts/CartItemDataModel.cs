namespace bcommerce_server.Infra.Data.Models.Carts;

public sealed record CartItemDataModel(
    Guid CartId,
    Guid ProductId,
    Guid? ColorId,
    decimal UnitPrice,
    int Quantity,
    DateTime AddedAt
);