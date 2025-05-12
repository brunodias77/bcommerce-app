// Infra/Data/Models/Carts/CartItemDataModel.cs
namespace bcommerce_server.Infra.Data.Models.Carts;

public sealed record CartItemDataModel(
    Guid Id,
    Guid CartId,
    Guid ProductId,
    Guid? ColorId,
    decimal UnitPrice,
    int Quantity,
    DateTime AddedAt
);


// namespace bcommerce_server.Infra.Data.Models.Carts;
//
// public sealed record CartItemDataModel(
//     Guid Id,
//     Guid CartId,
//     Guid ProductId,
//     int Quantity,
//     DateTime AddedAt
// );