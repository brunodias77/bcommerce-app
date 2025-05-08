using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Application.Products.GetAll;
using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Repostories;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.Infra.Repositories;

namespace bcommerce_server.Application.Products.GetById;

public class GetProductByIdUseCase : IGetProductByIdUseCase
{
    public GetProductByIdUseCase(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    public async Task<Result<GetProductByIdOutput, Notification>> Execute(GetProductByIdInput input)
    {
        var notification = Notification.Create();
        await BeginTransaction();
        Guid.TryParse(input.productId, out Guid productId);
        var product = await GetProductById(productId);
        product.Validate(notification);
        if (notification.HasError())
        {
            return Result<GetProductByIdOutput, Notification>.Fail(notification);
        }
        return Result<GetProductByIdOutput, Notification>.Ok(
            new GetProductByIdOutput(
                product.Name,
                product.Description,
                product.Price,
                product.OldPrice,
                product.Category?.Name, // null-safe
                product.StockQuantity,
                product.Sold,
                product.IsActive,
                product.Popular,
                product.CreatedAt,
                product.UpdatedAt,
                product.Images.Select(i => i.Url).ToList(), // ✅ usa i.Url
                product.Colors.Select(c => c.Color.Value).ToList(), // ✅ usa c.Color.Value
                product.Reviews.Select(r => new ReviewItemOutput(
                    r.Rating,
                    r.Comment,
                    r.CreatedAt
                )).ToList()
            )
        );

    }
    
    private async Task BeginTransaction()
    {
        await _unitOfWork.Begin();
    }

    private async Task<Product> GetProductById(Guid id)
    {
        return await _productRepository.Get(id, CancellationToken.None);
    }
}