namespace bcommerce_server.Infra.Data.Models.Orders;

public sealed record PaymentDataModel(
    Guid Id,
    Guid OrderId,
    string Method,
    decimal Amount,
    string Status,
    string? TransactionId,
    DateTime? PaidAt,
    DateTime CreatedAt
);