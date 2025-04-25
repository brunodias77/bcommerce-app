using bcommerce_server.Domain.Coupons.Entities;
using bcommerce_server.Domain.Coupons.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Coupons;


public static class CustomerCouponMapper
{
    public static CustomerCoupon ToDomain(CustomerCouponDataModel model)
    {
        return CustomerCoupon.With(
            CustomerCouponID.From(model.Id),
            model.CustomerId,
            CouponID.From(model.CouponId),
            model.AssignedAt,
            model.RedeemedAt
        );
    }

    public static CustomerCouponDataModel ToDataModel(CustomerCoupon entity)
    {
        return new CustomerCouponDataModel(
            entity.Id.Value,
            entity.CustomerId,
            entity.CouponId.Value,
            entity.AssignedAt,
            entity.RedeemedAt
        );
    }
}