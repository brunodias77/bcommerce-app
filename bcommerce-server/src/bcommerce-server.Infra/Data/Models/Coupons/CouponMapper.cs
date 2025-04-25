using bcommerce_server.Domain.Coupons;
using bcommerce_server.Domain.Coupons.Identifiers;
using bcommerce_server.Domain.Coupons.ValueObjects;

namespace bcommerce_server.Infra.Data.Models.Coupons;

public static class CouponMapper
{
    public static Coupon ToDomain(CouponDataModel model)
    {
        return Coupon.With(
            CouponID.From(model.Id),
            model.Code,
            DiscountType.From(model.DiscountType),
            model.DiscountValue,
            model.ValidFrom,
            model.ValidTo,
            model.UsageCount,
            model.MaxUsage,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public static CouponDataModel ToDataModel(Coupon coupon)
    {
        return new CouponDataModel(
            coupon.Id.Value,
            coupon.Code,
            coupon.DiscountType.Value,
            coupon.DiscountValue,
            coupon.ValidFrom,
            coupon.ValidTo,
            coupon.UsageCount,
            coupon.MaxUsage,
            coupon.CreatedAt,
            coupon.UpdatedAt
        );
    }
}