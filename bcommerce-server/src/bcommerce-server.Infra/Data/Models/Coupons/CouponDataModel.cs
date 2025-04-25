namespace bcommerce_server.Infra.Data.Models.Coupons;

public sealed record CouponDataModel(
    Guid Id,
    string Code,
    string DiscountType,
    decimal DiscountValue,
    DateTime ValidFrom,
    DateTime ValidTo,
    int UsageCount,
    int? MaxUsage,
    DateTime CreatedAt,
    DateTime UpdatedAt
);