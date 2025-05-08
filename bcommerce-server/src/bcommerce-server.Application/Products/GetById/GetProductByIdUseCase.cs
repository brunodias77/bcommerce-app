using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Application.Products.GetAll;
using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Repostories;
using bcommerce_server.Domain.Validations;
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

        try
        {
            await BeginTransaction();

            if (!Guid.TryParse(input.productId, out Guid productId))
            {
                return Fail("ID de produto inválido.");
            }

            var product = await GetProductById(productId);
            if (product == null)
            {
                return Fail("Produto não encontrado.");
            }

            product.Validate(notification);

            if (notification.HasError())
            {
                return Result<GetProductByIdOutput, Notification>.Fail(notification);
            }

            await _unitOfWork.Commit(); // commit only on success

            return Success(product);
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback(); // garante rollback da transação em caso de exceção
            return Fail("Erro interno ao buscar produto: " + ex.Message);
        }
    }
    
    private async Task BeginTransaction()
    {
        await _unitOfWork.Begin();
    }

    private async Task<Product> GetProductById(Guid id)
    {
        return await _productRepository.Get(id, CancellationToken.None);
    }
    
    private Result<GetProductByIdOutput, Notification> Success(Product product)
    {
        return Result<GetProductByIdOutput, Notification>.Ok(new GetProductByIdOutput(
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
        ));
    }

    private Result<GetProductByIdOutput, Notification> Fail(string message)
    {
        return Result<GetProductByIdOutput, Notification>.Fail(Notification.Create(new Error(message)));
    }
}