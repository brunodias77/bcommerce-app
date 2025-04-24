using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Repostories;
using bcommerce_server.Infra.Data.Models.Products;
using bcommerce_server.Infra.Repositories;
using Dapper;

namespace bcommerce_server.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    public ProductRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork _unitOfWork;

    public Task Insert(Product aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Product> Get(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Product aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(Product aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> GetAllProducts(CancellationToken cancellationToken)
    {
        const string sql = @"
                        SELECT
                            p.id,
                            p.name,
                            p.description,
                            p.price,
                            p.old_price,
                            p.stock_quantity,
                            p.sold,
                            p.is_active,
                            p.popular,
                            p.created_at,
                            p.updated_at,

                            c.id AS category_id,
                            c.name AS category_name,

                            COALESCE(json_agg(DISTINCT pi.url) FILTER (WHERE pi.url IS NOT NULL), '[]') AS images_json,
                            COALESCE(json_agg(DISTINCT pc.color_value) FILTER (WHERE pc.color_value IS NOT NULL), '[]') AS colors_json

                        FROM products p
                        JOIN categories c ON p.category_id = c.id
                        LEFT JOIN product_images pi ON p.id = pi.product_id
                        LEFT JOIN product_colors pc ON p.id = pc.product_id

                        GROUP BY
                            p.id, p.name, p.description, p.price, p.old_price, p.stock_quantity,
                            p.sold, p.is_active, p.popular, p.created_at, p.updated_at,
                            c.id, c.name

                        ORDER BY p.created_at DESC;
                    ";

        var conn = _unitOfWork.Connection;

        var dataModels = await conn.QueryAsync<ProductDataModel>(sql);

        
        foreach (var model in dataModels)
        {
            Console.WriteLine($"Categoria mapeada: {model.CategoryName}"); // deve imprimir "Watches"
        }

        var products = dataModels.Select(p => p.ToDomain()).ToList();

        return products;
    }
}