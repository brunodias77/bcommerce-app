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
            // iniciar a transacao
            await _unitOfWork.Begin();
            //muda o id para guid
            if (!Guid.TryParse(input.productId, out Guid productId))
            {
                return Fail("ID de produto inválido.");
            }
            // pegar o pronduct pelo repository
            var product = await _productRepository.Get(productId, CancellationToken.None);
            // validar
            product.Validate(notification);
            // retornar erro
            if (notification.HasError())
            {
                return Result<GetProductByIdOutput, Notification>.Fail(notification);
            }
            // retorno
            var productOutput = new GetProductByIdOutput(
                ProductId: product.Id.Value,
                    Name: product.Name,
                    Description: product.Description,
                    Price: product.Price,
                    OldPrice: product.OldPrice,
                    CategoryId: product.CategoryId.Value,
                    CategoryName: product.Category?.Name,
                    StockQuantity: product.StockQuantity,
                    Sold: product.Sold,
                    IsActive: product.IsActive,
                    Popular: product.Popular,
                    CreatedAt: product.CreatedAt,
                    Images: product.Images.Select(i => i.Url).ToList(),
                    Colors: product.Colors.Select(c => new ColorItemOutput(
                        Name: c.Color.Name,
                        Value: c.Color.Value
                    )).ToList(),
                    Reviews: product.Reviews.Select(r => new ReviewItemOutput(
                        Rating: r.Rating,
                        Comment: r.Comment,
                        CreatedAt: r.CreatedAt
                    )).ToList()
            );

            await _unitOfWork.Commit();

            // idem aqui: Ok<TSuccess=IList<...>,TError=Notification>
            return Result<GetProductByIdOutput, Notification>.Ok(productOutput);
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback(); // garante rollback da transação em caso de exceção
            return Fail("Erro interno ao buscar produto: " + ex.Message);
        }
    }
    private Result<GetProductByIdOutput, Notification> Fail(string message)
    {
        return Result<GetProductByIdOutput, Notification>.Fail(Notification.Create(new Error(message)));
    }
}