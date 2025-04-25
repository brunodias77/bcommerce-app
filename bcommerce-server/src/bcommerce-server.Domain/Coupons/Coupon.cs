using bcommerce_server.Domain.Coupons.Identifiers;
using bcommerce_server.Domain.Coupons.Validators;
using bcommerce_server.Domain.Coupons.ValueObjects;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Coupons;

public class Coupon : AggregateRoot<CouponID>
{
    private string _code;
    private DiscountType _discountType;
    private decimal _discountValue;
    private DateTime _validFrom;
    private DateTime _validTo;
    private int _usageCount;
    private int? _maxUsage;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    private Coupon(
        CouponID id,
        string code,
        DiscountType discountType,
        decimal discountValue,
        DateTime validFrom,
        DateTime validTo,
        int usageCount,
        int? maxUsage,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _code = code;
        _discountType = discountType;
        _discountValue = discountValue;
        _validFrom = validFrom;
        _validTo = validTo;
        _usageCount = usageCount;
        _maxUsage = maxUsage;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public static Coupon Create(
        string code,
        DiscountType discountType,
        decimal discountValue,
        DateTime validFrom,
        DateTime validTo,
        int? maxUsage = null
    )
    {
        var now = DateTime.UtcNow;

        return new Coupon(
            CouponID.Generate(),
            code,
            discountType,
            discountValue,
            validFrom,
            validTo,
            0, // usageCount começa em 0
            maxUsage,
            now,
            now
        );
    }

    public static Coupon With(
        CouponID id,
        string code,
        DiscountType discountType,
        decimal discountValue,
        DateTime validFrom,
        DateTime validTo,
        int usageCount,
        int? maxUsage,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new Coupon(
            id,
            code,
            discountType,
            discountValue,
            validFrom,
            validTo,
            usageCount,
            maxUsage,
            createdAt,
            updatedAt
        );
    }

    public Coupon Update(
        string code,
        DiscountType discountType,
        decimal discountValue,
        DateTime validFrom,
        DateTime validTo,
        int? maxUsage = null
    )
    {
        _code = code;
        _discountType = discountType;
        _discountValue = discountValue;
        _validFrom = validFrom;
        _validTo = validTo;
        _maxUsage = maxUsage;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public void IncrementUsage()
    {
        _usageCount++;
        _updatedAt = DateTime.UtcNow;
    }

    public bool IsValid(DateTime now)
    {
        return now >= _validFrom && now <= _validTo;
    }

    public override void Validate(IValidationHandler handler)
    {
        new CouponValidator(this, handler).Validate();
    }

    // 🧱 Propriedades públicas (read-only)
    public string Code => _code;
    public DiscountType DiscountType => _discountType;
    public decimal DiscountValue => _discountValue;
    public DateTime ValidFrom => _validFrom;
    public DateTime ValidTo => _validTo;
    public int UsageCount => _usageCount;
    public int? MaxUsage => _maxUsage;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;

    public bool IsExpired => DateTime.UtcNow > _validTo;
    public bool HasUsageLeft => !_maxUsage.HasValue || _usageCount < _maxUsage.Value;
}