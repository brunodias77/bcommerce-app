using bcommerce_server.Domain.Common;
using bcommerce_server.Domain.Orders.Entities;
using bcommerce_server.Domain.Orders.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Orders;


public static class PaymentMapper
{
    public static Payment ToDomain(PaymentDataModel model)
    {
        return Payment.With(
            PaymentID.From(model.Id),
            model.Method,
            model.Amount,
            Status.Payment(model.Status),
            model.TransactionId,
            model.PaidAt,
            model.CreatedAt
        );
    }

    public static PaymentDataModel ToDataModel(Payment payment, Guid orderId)
    {
        return new PaymentDataModel(
            payment.Id.Value,
            orderId, // ✅ necessário para persistência
            payment.Method,
            payment.Amount,
            payment.Status.Value,
            payment.TransactionId,
            payment.PaidAt,
            payment.CreatedAt
        );
    }
}