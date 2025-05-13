using bcommerce_server.Domain.Carts.Entities;
using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Carts.Repositories;

public interface ICartRepository : IGenericRepository<Cart>
{
    // Aqui você pode adicionar métodos específicos se necessário, ex:
    // Task<Cart?> GetByCustomerId(Guid customerId, CancellationToken cancellationToken);
    Task<Cart?> GetByCustomerId(Guid customerId, CancellationToken cancellationToken);
    Task<IEnumerable<CartItem>> GetCartItemsByCartId(Guid cartId, CancellationToken cancellationToken);
}