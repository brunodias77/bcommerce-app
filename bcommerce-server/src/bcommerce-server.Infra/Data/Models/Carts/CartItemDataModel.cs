namespace bcommerce_server.Infra.Data.Models.Carts;

public sealed record CartItemDataModel(
    Guid Id,
    Guid CartId,
    Guid ProductId,
    int Quantity,
    DateTime AddedAt
);