using bcommerce_server.Domain.Common;
using bcommerce_server.Domain.Orders.Entities;
using bcommerce_server.Domain.Orders.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Orders;

public static class ShipmentMapper
{
    public static Shipment ToDomain(ShipmentDataModel model)
    {
        return Shipment.With(
            ShipmentID.From(model.Id),
            model.Carrier,
            model.TrackingNumber,
            Status.Shipment(model.Status), // ✅ Corrigido
            model.ShippedAt,
            model.DeliveredAt,
            model.CreatedAt
        );
    }

    public static ShipmentDataModel ToDataModel(Shipment shipment, Guid orderId)
    {
        return new ShipmentDataModel(
            shipment.Id.Value,
            orderId,
            shipment.Carrier,
            shipment.TrackingNumber,
            shipment.Status.Value,
            shipment.ShippedAt,
            shipment.DeliveredAt,
            shipment.CreatedAt
        );
    }
}
