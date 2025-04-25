using bcommerce_server.Domain.Orders.Entities;
using bcommerce_server.Domain.Orders.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Orders;

public static class OrderItemMapper
{
    public static OrderItem ToDomain(OrderItemDataModel model)
    {
        return OrderItem.With(
            OrderItemID.From(model.Id),
            model.ProductId,       // ✅ Removido model.OrderId
            model.Quantity,
            model.UnitPrice,
            model.CreatedAt
        );
    }

    public static OrderItemDataModel ToDataModel(OrderItem item, Guid orderId)
    {
        return new OrderItemDataModel(
            item.Id.Value,
            orderId,               // ✅ Precisamos passar o OrderId ao persistir
            item.ProductId,
            item.Quantity,
            item.UnitPrice,
            item.CreatedAt
        );
    }
}
