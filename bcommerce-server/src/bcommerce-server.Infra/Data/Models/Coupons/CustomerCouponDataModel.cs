namespace bcommerce_server.Infra.Data.Models.Coupons;

public sealed record CustomerCouponDataModel(
    Guid Id,
    Guid CustomerId,
    Guid CouponId,
    DateTime AssignedAt,
    DateTime? RedeemedAt
);