using bcommerce_server.Domain.Coupons.Identifiers;
using bcommerce_server.Domain.Coupons.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Coupons.Entities;

public class CustomerCoupon : Entity<CustomerCouponID>
{
    private Guid _customerId;
    private CouponID _couponId;
    private DateTime _assignedAt;
    private DateTime? _redeemedAt;

    private CustomerCoupon(
        CustomerCouponID id,
        Guid customerId,
        CouponID couponId,
        DateTime assignedAt,
        DateTime? redeemedAt
    ) : base(id)
    {
        _customerId = customerId;
        _couponId = couponId;
        _assignedAt = assignedAt;
        _redeemedAt = redeemedAt;
    }

    public static CustomerCoupon Create(Guid customerId, CouponID couponId)
    {
        var now = DateTime.UtcNow;
        return new CustomerCoupon(
            CustomerCouponID.Generate(),
            customerId,
            couponId,
            now,
            null
        );
    }

    public static CustomerCoupon With(
        CustomerCouponID id,
        Guid customerId,
        CouponID couponId,
        DateTime assignedAt,
        DateTime? redeemedAt
    )
    {
        return new CustomerCoupon(id, customerId, couponId, assignedAt, redeemedAt);
    }

    public CustomerCoupon Redeem()
    {
        if (_redeemedAt is null)
        {
            _redeemedAt = DateTime.UtcNow;
        }
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new CustomerCouponValidator(this, handler).Validate();
    }

    // Propriedades públicas (read-only)
    public Guid CustomerId => _customerId;
    public CouponID CouponId => _couponId;
    public DateTime AssignedAt => _assignedAt;
    public DateTime? RedeemedAt => _redeemedAt;
}