using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Products.Repostories;
using bcommerce_server.Domain.Validations.Handlers;

namespace bcommerce_server.Application.Products.GetAll
{
    public class GetAllProductsUseCase : IGetAllProuctsUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<List<GetAllProductsOutput>, Notification>> Execute(Unit input)
        {
            // var products = await _productRepository.GetAll(CancellationToken.None);
            // var outputs = products.Select(product => new GetAllProductsOutput(
            //     product.Id.Value,
            //     product.Name,
            //     product.Description,
            //     product.Price,
            //     product.OldPrice,
            //     product.StockQuantity,
            //     product.Sold,
            //     product.IsActive,
            //     product.Popular,
            //     product.CreatedAt,
            //     product.UpdatedAt
            // )).ToList();

            // return Result<List<GetAllProductsOutput>, Notification>.Ok(outputs);
            var products = await _productRepository.GetAll(CancellationToken.None);
            var notification = Notification.Create();

            foreach (var product in products)
            {
                product.Validate(notification); // Valida produto com ProductValidator
            }

            if (notification.HasError())
                return Result<List<GetAllProductsOutput>, Notification>.Fail(notification);

            var outputs = products.Select(product => new GetAllProductsOutput(
                product.Id.Value,
                product.Name,
                product.Description,
                product.Price,
                product.OldPrice,
                product.CategoryId.Value,           // ✅ Adicionado
                product.StockQuantity,
                product.Sold,
                product.IsActive,
                product.Popular,
                product.CreatedAt,
                product.UpdatedAt
            )).ToList();

            return Result<List<GetAllProductsOutput>, Notification>.Ok(outputs);
        }
    }
}