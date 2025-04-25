using bcommerce_server.Domain.Coupons;
using bcommerce_server.Domain.Coupons.Repositories;
using bcommerce_server.Infra.Data.Models.Coupons;
using Dapper;

namespace bcommerce_server.Infra.Repositories;

public class DapperCouponRepository : ICouponRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private ICouponRepository _couponRepositoryImplementation;

    public DapperCouponRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Insert(Coupon coupon, CancellationToken cancellationToken)
    {
        var model = CouponMapper.ToDataModel(coupon);
        const string sql = @"
            INSERT INTO coupons (
                id, code, discount_type, discount_value, 
                valid_from, valid_to, usage_count, max_usage, 
                created_at, updated_at
            ) VALUES (
                @Id, @Code, @DiscountType, @DiscountValue, 
                @ValidFrom, @ValidTo, @UsageCount, @MaxUsage, 
                @CreatedAt, @UpdatedAt
            );
        ";

        await _unitOfWork.Connection.ExecuteAsync(sql, model, _unitOfWork.Transaction);
    }

    public async Task<Coupon?> Get(Guid id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM coupons WHERE id = @Id;";
        var model = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<CouponDataModel>(
            sql, new { Id = id }, _unitOfWork.Transaction);

        return model is null ? null : CouponMapper.ToDomain(model);
    }

    public async Task<IEnumerable<Coupon>> GetAll(CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM coupons;";
        var models = await _unitOfWork.Connection.QueryAsync<CouponDataModel>(sql, transaction: _unitOfWork.Transaction);
        return models.Select(CouponMapper.ToDomain);
    }

    public async Task Update(Coupon coupon, CancellationToken cancellationToken)
    {
        var model = CouponMapper.ToDataModel(coupon);

        const string sql = @"
            UPDATE coupons SET
                code = @Code,
                discount_type = @DiscountType,
                discount_value = @DiscountValue,
                valid_from = @ValidFrom,
                valid_to = @ValidTo,
                usage_count = @UsageCount,
                max_usage = @MaxUsage,
                updated_at = @UpdatedAt
            WHERE id = @Id;
        ";

        await _unitOfWork.Connection.ExecuteAsync(sql, model, _unitOfWork.Transaction);
    }

    public async Task<Coupon?> GetByCode(string code, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM coupons WHERE code = @Code;";
        var model = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<CouponDataModel>(
            sql, new { Code = code }, _unitOfWork.Transaction);

        return model is null ? null : CouponMapper.ToDomain(model);
    }

    public async Task Delete(Coupon coupon, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM coupons WHERE id = @Id;";
        await _unitOfWork.Connection.ExecuteAsync(sql, new { Id = coupon.Id.Value }, _unitOfWork.Transaction);
    }
}