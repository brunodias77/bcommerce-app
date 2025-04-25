using bcommerce_server.Domain.Carts;
using bcommerce_server.Domain.Carts.Entities;
using bcommerce_server.Domain.Carts.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Carts;

public static class CartMapper
{
    public static Cart ToDomain(CartDataModel model, List<CartItem> items)
    {
        return Cart.With(
            CartID.From(model.Id),
            model.CustomerId,
            items,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public static CartDataModel ToDataModel(Cart cart)
    {
        return new CartDataModel(
            cart.Id.Value,
            cart.CustomerId,
            cart.CreatedAt,
            cart.UpdatedAt
        );
    }
}