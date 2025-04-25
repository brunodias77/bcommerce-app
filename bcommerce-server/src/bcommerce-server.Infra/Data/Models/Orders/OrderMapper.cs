using bcommerce_server.Domain.Common;
using bcommerce_server.Domain.Orders;
using bcommerce_server.Domain.Orders.Entities;
using bcommerce_server.Domain.Orders.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Orders;

public static class OrderMapper
{
    public static Order ToDomain(
        OrderDataModel model,
        List<OrderItem> items,
        Payment? payment,
        Shipment? shipment
    )
    {
        return Order.With(
            OrderID.From(model.Id),
            model.CustomerId,
            model.ShippingAddressId,
            model.CouponId,
            Status.Order(model.Status), // ✅ Correto
            model.TotalAmount,
            items,
            payment,
            shipment,
            model.PlacedAt,
            model.ShippedAt,
            model.DeliveredAt,
            model.CreatedAt,
            model.UpdatedAt
        );
    }


    public static OrderDataModel ToDataModel(Order order)
    {
        return new OrderDataModel(
            order.Id.Value,
            order.CustomerId,
            order.ShippingAddressId,
            order.CouponId,
            order.Status.Value,
            order.TotalAmount,
            order.PlacedAt,
            order.ShippedAt,
            order.DeliveredAt,
            order.CreatedAt,
            order.UpdatedAt
        );
    }
}
