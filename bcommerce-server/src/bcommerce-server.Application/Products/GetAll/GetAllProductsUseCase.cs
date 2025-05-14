using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Products.Repostories;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.Domain.Validations;
using bcommerce_server.Infra.Repositories;

namespace bcommerce_server.Application.Products.GetAll
{
    public class GetAllProductsUseCase : IGetAllProuctsUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsUseCase(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IList<GetAllProductItemOutput>, Notification>> Execute(Unit input)
        {
            var notification = Notification.Create();
            await _unitOfWork.Begin();

            var products = await _productRepository.GetAll(CancellationToken.None);
            foreach (var product in products)
                product.Validate(notification);

            if (notification.HasError())
            {
                await _unitOfWork.Rollback();
                // agora Fail<TSuccess=IList<...>,TError=Notification>
                return Result<IList<GetAllProductItemOutput>, Notification>.Fail(notification);
            }

            var items = products
                .Select(p => new GetAllProductItemOutput(
                    Name: p.Name,
                    Description: p.Description,
                    Price: p.Price,
                    OldPrice: p.OldPrice,
                    CategoryId: p.CategoryId.Value,
                    CategoryName: p.Category?.Name,
                    StockQuantity: p.StockQuantity,
                    Sold: p.Sold,
                    IsActive: p.IsActive,
                    Popular: p.Popular,
                    Images: p.Images.Select(i => i.Url).ToList(),
                    Colors: p.Colors.Select(c => new ColorItemOutput(
                        Name: c.Color.Name,
                        Value: c.Color.Value
                    )).ToList(),
                    Reviews: p.Reviews.Select(r => new ReviewItemOutput(
                        Rating: r.Rating,
                        Comment: r.Comment,
                        CreatedAt: r.CreatedAt
                    )).ToList()
                ))
                .ToList(); // List<GetAllProductItemOutput> implementa IList<>

            await _unitOfWork.Commit();

            // idem aqui: Ok<TSuccess=IList<...>,TError=Notification>
            return Result<IList<GetAllProductItemOutput>, Notification>.Ok(items);
        }
    }
}


