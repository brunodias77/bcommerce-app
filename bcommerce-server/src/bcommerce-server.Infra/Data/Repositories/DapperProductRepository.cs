using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Products.Repostories;
using bcommerce_server.Infra.Data.Models.Products;
using Dapper;

namespace bcommerce_server.Infra.Repositories;

public class DapperProductRepository : IProductRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public DapperProductRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Insert(Product aggregate, CancellationToken cancellationToken)
    {
        var model = ProductMapper.ToDataModel(aggregate);

        const string sql = @"
            INSERT INTO products (
                id, name, description, price, old_price, category_id, stock_quantity,
                sold, is_active, popular, deleted_at, created_at, updated_at
            ) VALUES (
                @Id, @Name, @Description, @Price, @OldPrice, @CategoryId, @StockQuantity,
                @Sold, @IsActive, @Popular, @DeletedAt, @CreatedAt, @UpdatedAt
            );
        ";

        await _unitOfWork.Connection.ExecuteAsync(sql, model, _unitOfWork.Transaction);
    }

    public async Task<Product?> Get(Guid id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM products WHERE id = @Id;";
        var model = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<ProductDataModel>(
            sql, new { Id = id }, _unitOfWork.Transaction);

        if (model == null) return null;

        var images = await GetImages(model.Id);
        var colors = await GetColors(model.Id);
        var reviews = await GetReviews(model.Id); // ✅ corrigido
        var category = await GetCategory(model.CategoryId); // ✅ carregando a Category

        return ProductMapper.ToDomain(model, images, colors, reviews, category); // ✅ repassando a Category
    }

    public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM products;";
        var models = await _unitOfWork.Connection.QueryAsync<ProductDataModel>(sql, transaction: _unitOfWork.Transaction);

        var products = new List<Product>();
        foreach (var model in models)
        {
            var images = await GetImages(model.Id);
            var colors = await GetColors(model.Id);
            var reviews = await GetReviews(model.Id); // ✅ corrigido
            var category = await GetCategory(model.CategoryId); // ✅ corrigido

            products.Add(ProductMapper.ToDomain(model, images, colors, reviews, category));
        }

        return products;
    }


    public async Task<IEnumerable<Product>> GetByCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM products WHERE category_id = @CategoryId;";
        var models = await _unitOfWork.Connection.QueryAsync<ProductDataModel>(
            sql, new { CategoryId = categoryId }, _unitOfWork.Transaction);

        var products = new List<Product>();
        foreach (var model in models)
        {
            var images = await GetImages(model.Id);
            var colors = await GetColors(model.Id);
            var reviews = await GetReviews(model.Id);

            products.Add(ProductMapper.ToDomain(model, images, colors, reviews));
        }

        return products;
    }
    
    // public async Task<IEnumerable<Product>> GetByCategory(Guid categoryId, CancellationToken cancellationToken)
    // {
    //     const string sql = "SELECT * FROM products WHERE category_id = @CategoryId;";
    //     var models = await _unitOfWork.Connection.QueryAsync<ProductDataModel>(
    //         sql, new { CategoryId = categoryId }, _unitOfWork.Transaction);
    //
    //     var category = await GetCategory(categoryId); // ✅ carregar uma vez só
    //
    //     var products = new List<Product>();
    //     foreach (var model in models)
    //     {
    //         var images = await GetImages(model.Id);
    //         var colors = await GetColors(model.Id);
    //         var reviews = await GetReviews(model.Id);
    //
    //         products.Add(ProductMapper.ToDomain(model, images, colors, reviews, category));
    //     }
    //
    //     return products;
    // }

    public async Task Update(Product aggregate, CancellationToken cancellationToken)
    {
        var model = ProductMapper.ToDataModel(aggregate);

        const string sql = @"
            UPDATE products
            SET name = @Name,
                description = @Description,
                price = @Price,
                old_price = @OldPrice,
                category_id = @CategoryId,
                stock_quantity = @StockQuantity,
                sold = @Sold,
                is_active = @IsActive,
                popular = @Popular,
                deleted_at = @DeletedAt,
                updated_at = @UpdatedAt
            WHERE id = @Id;
        ";

        await _unitOfWork.Connection.ExecuteAsync(sql, model, _unitOfWork.Transaction);
    }

    public async Task Delete(Product aggregate, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM products WHERE id = @Id;";
        await _unitOfWork.Connection.ExecuteAsync(sql, new { Id = aggregate.Id.Value }, _unitOfWork.Transaction);
    }

    // Métodos auxiliares para carregar imagens, cores e avaliações
    private async Task<List<ProductImage>> GetImages(Guid productId)
    {
        const string sql = "SELECT * FROM product_images WHERE product_id = @ProductId;";
        var models = await _unitOfWork.Connection.QueryAsync<ProductImageDataModel>(sql, new { ProductId = productId }, _unitOfWork.Transaction);
        return models.Select(ProductImageMapper.ToDomain).ToList();
    }

    private async Task<List<ProductColor>> GetColors(Guid productId)
    {
        const string sql = @"
        SELECT 
            pc.id AS Id,
            pc.product_id AS ProductId,
            pc.color_id AS ColorId,
            c.name AS ColorName,
            c.value AS ColorValue,
            pc.created_at AS CreatedAt,
            pc.updated_at AS UpdatedAt
        FROM product_colors pc
        INNER JOIN colors c ON pc.color_id = c.id
        WHERE pc.product_id = @ProductId;
    ";

        var models = await _unitOfWork.Connection.QueryAsync<ProductColorDataModel>(
            sql,
            new { ProductId = productId },
            _unitOfWork.Transaction
        );

        return models.Select(ProductColorMapper.ToDomain).ToList();
    }


    private async Task<List<ProductReview>> GetReviews(Guid productId)
    {
        const string sql = "SELECT * FROM product_reviews WHERE product_id = @ProductId;";
        var models = await _unitOfWork.Connection.QueryAsync<ProductReviewDataModel>(sql, new { ProductId = productId }, _unitOfWork.Transaction);
        return models.Select(ProductReviewMapper.ToDomain).ToList();
    }

    private async Task<Category> GetCategory(Guid categoryId)
    {
        const string sql = "SELECT * FROM categories WHERE id = @CategoryId;";
        var models = await _unitOfWork.Connection.QueryAsync<CategoryDataModel>(sql, new { CategoryId = categoryId }, _unitOfWork.Transaction);
        return models.Select(CategoryMapper.ToDomain).FirstOrDefault();
    }
}
