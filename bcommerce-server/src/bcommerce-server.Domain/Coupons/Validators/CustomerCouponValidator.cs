using bcommerce_server.Domain.Coupons.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Coupons.Validators;

public class CustomerCouponValidator : Validator
{
    private readonly CustomerCoupon _customerCoupon;

    public CustomerCouponValidator(CustomerCoupon customerCoupon, IValidationHandler handler)
        : base(handler)
    {
        _customerCoupon = customerCoupon ?? throw new ArgumentNullException(nameof(customerCoupon));
    }

    public override void Validate()
    {
        ValidateCustomerId();
        ValidateCouponId();
        ValidateDates();
    }

    private void ValidateCustomerId()
    {
        if (_customerCoupon.CustomerId == Guid.Empty)
            AddError("'customerId' não pode ser vazio.");
    }

    private void ValidateCouponId()
    {
        if (_customerCoupon.CouponId is null)
            AddError("'couponId' não pode ser nulo.");
    }

    private void ValidateDates()
    {
        if (_customerCoupon.RedeemedAt.HasValue &&
            _customerCoupon.RedeemedAt.Value < _customerCoupon.AssignedAt)
        {
            AddError("'redeemedAt' não pode ser anterior a 'assignedAt'.");
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}