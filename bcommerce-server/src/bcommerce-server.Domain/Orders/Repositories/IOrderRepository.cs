using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Orders.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetByCustomer(Guid customerId, CancellationToken cancellationToken);
}