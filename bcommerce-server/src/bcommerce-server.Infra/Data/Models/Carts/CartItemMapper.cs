using bcommerce_server.Domain.Carts.Entities;

namespace bcommerce_server.Infra.Data.Models.Carts;

public static class CartItemMapper
{
    public static CartItem ToDomain(CartItemDataModel m)
        => CartItem.With(
            m.ProductId,
            m.ColorId,
            m.UnitPrice,
            m.Quantity,
            m.AddedAt
        );

    public static CartItemDataModel ToDataModel(CartItem item, Guid cartId)
        => new CartItemDataModel(
            CartId: cartId,
            ProductId: item.ProductId,
            ColorId: item.ColorId,
            UnitPrice: item.UnitPrice,
            Quantity: item.Quantity,
            AddedAt: item.AddedAt
        );
}


// using bcommerce_server.Domain.Carts.Entities;
// using bcommerce_server.Domain.Carts.Identifiers;
//
// namespace bcommerce_server.Infra.Data.Models.Carts;
//
// public static class CartItemMapper
// {
//     public static CartItem ToDomain(CartItemDataModel model)
//     {
//         return CartItem.With(
//             CartItemID.From(model.Id),
//             model.ProductId,
//             model.Quantity,
//             model.AddedAt
//         );
//     }
//
//     public static CartItemDataModel ToDataModel(CartItem item, Guid cartId)
//     {
//         return new CartItemDataModel(
//             item.Id.Value,
//             cartId,
//             item.ProductId,
//             item.Quantity,
//             item.AddedAt
//         );
//     }
// }