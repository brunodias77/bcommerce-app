using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Coupons.Repositories;

public interface ICouponRepository : IGenericRepository<Coupon>
{
    // Aqui você pode adicionar buscas por código, por validade, etc.
    Task<Coupon?> GetByCode(string code, CancellationToken cancellationToken);
}