
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Repostories;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.Infra.Repositories;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Application.Products.GetAll
{
    public class GetAllProductsUseCase : IGetAllProuctsUseCase
    {
        public GetAllProductsUseCase(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;   

        public async Task<Result<GetAllProductsOutput, Notification>> Execute(Unit input)
        {
            var notification = Notification.Create();
            // Iniciar a transação
            await BeginTransaction();

            // Buscar os produtos
            var products = await GetAllProducts();

            // Validar produtos
            foreach (var product in products)
            {
                product.Validate(notification);
            }

            // Se houver erro na validação, falha a operação
            if (notification.HasError())
            {
                await _unitOfWork.Rollback(); // Rollback caso ocorra erro na validação
                return Result<GetAllProductsOutput, Notification>.Fail(notification);
            }

            // Mapear os produtos para o formato de saída
            var productItems = products
                .Select(product => CreateProductItemOutput(product))
                .ToList();

            // Criar a saída com a lista de produtos
            var output = new GetAllProductsOutput(productItems);

            // Commitar a transação
            await _unitOfWork.Commit();

            // Retornar o sucesso com os dados
            return Result<GetAllProductsOutput, Notification>.Ok(output);
        }
        
        private async Task BeginTransaction()
        {
            await _unitOfWork.Begin();
        }

        private async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAll(CancellationToken.None);
        }
        private static GetAllProductItemOutput CreateProductItemOutput(Product product)
        {
            return new GetAllProductItemOutput(
                Id: product.Id.Value,
                Name: product.Name,
                Description: product.Description,
                Price: product.Price,
                OldPrice: product.OldPrice,
                CategoryId: product.CategoryId.Value,
                StockQuantity: product.StockQuantity,
                Sold: product.Sold,
                IsActive: product.IsActive,
                Popular: product.Popular,
                CreatedAt: product.CreatedAt,
                UpdatedAt: product.UpdatedAt,
                Images: product.Images.Select(img => img.Url).ToList(),
                Colors: product.Colors.Select(color => color.Color.Value).ToList(),
                Reviews: product.Reviews
                    .Select(review => new ReviewItemOutput(
                        Rating: review.Rating,
                        Comment: review.Comment,
                        CreatedAt: review.CreatedAt
                    ))
                    .ToList()
            );
        }
    }
}