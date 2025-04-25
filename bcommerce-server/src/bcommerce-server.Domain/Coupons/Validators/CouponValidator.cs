using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Coupons.Validators;

public class CouponValidator : Validator
{
    private const int CODE_MIN_LENGTH = 3;
    private const int CODE_MAX_LENGTH = 50;

    private readonly Coupon _coupon;

    public CouponValidator(Coupon coupon, IValidationHandler handler)
        : base(handler)
    {
        _coupon = coupon ?? throw new ArgumentNullException(nameof(coupon));
    }

    public override void Validate()
    {
        ValidateCode();
        ValidateDiscount();
        ValidateDates();
    }

    private void ValidateCode()
    {
        var code = _coupon.Code;

        if (string.IsNullOrWhiteSpace(code))
        {
            AddError("'código' do cupom não pode estar em branco.");
            return;
        }

        var length = code.Trim().Length;
        if (length < CODE_MIN_LENGTH || length > CODE_MAX_LENGTH)
        {
            AddError($"'código' deve ter entre {CODE_MIN_LENGTH} e {CODE_MAX_LENGTH} caracteres.");
        }
    }

    private void ValidateDiscount()
    {
        var value = _coupon.DiscountValue;
        var type = _coupon.DiscountType.Value;

        if (type == "percent")
        {
            if (value < 0 || value > 100)
                AddError("'desconto percentual' deve estar entre 0 e 100.");
        }
        else if (type == "fixed")
        {
            if (value < 0)
                AddError("'desconto fixo' não pode ser negativo.");
        }
        else
        {
            AddError("'tipo de desconto' inválido.");
        }
    }

    private void ValidateDates()
    {
        if (_coupon.ValidTo < _coupon.ValidFrom)
        {
            AddError("'validTo' deve ser maior ou igual a 'validFrom'.");
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}