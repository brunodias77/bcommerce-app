using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.Repostories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetAllProducts(CancellationToken cancellationToken);
}