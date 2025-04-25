using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.Repostories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategory(Guid categoryId, CancellationToken cancellationToken);
}