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
        private readonly IProductRepository         _productRepository;
        private readonly IUnitOfWork                _unitOfWork;

        public GetAllProductsUseCase(
            IProductRepository         productRepository,
            IUnitOfWork                unitOfWork)
        {
            _productRepository      = productRepository;
            _unitOfWork             = unitOfWork;
        }

        public async Task<Result<GetAllProductItemOutput, Notification>> Execute(Unit input)
        {
            return null;
            // var notification = Notification.Create();
            // await _unitOfWork.Begin();
            //
            // // 1) Busca todos os produtos
            // var products = await _productRepository.GetAll(CancellationToken.None);
            //
            // // 2) Valida cada produto
            // foreach (var product in products)
            //     product.Validate(notification);
            //
            // if (notification.HasError())
            // {
            //     await _unitOfWork.Rollback();
            //     return Result<GetAllProductItemOutput, Notification>.Fail(notification);
            // }
            //
            // // 3) Para cada produto, busca também suas cores (nome + value)
            // var items = new List<GetAllProductItemOutput>();
            // foreach (var p in products)
            // {
            //     var imageUrls = p.Images.Select(i => i.Url).ToList();
            //
            //     // Aqui chamamos o repositório de cores que faz o JOIN com colors
            //     var colorDetails = await _productColorRepository
            //         .GetColorsByProductId(p.Id.Value, CancellationToken.None);
            //
            //     var colorOutputs = colorDetails
            //         .Select(c => new ColorItemOutput(
            //             Id:    c.ColorId,
            //             Name:  c.ColorName,
            //             Value: c.ColorValue))
            //         .ToList();
            //
            //     var reviewOutputs = p.Reviews
            //         .Select(r => new ReviewItemOutput(
            //             Rating:    r.Rating,
            //             Comment:   r.Comment,
            //             CreatedAt: r.CreatedAt))
            //         .ToList();
            //
            //     items.Add(new GetAllProductItemOutput(
            //         Id:           p.Id.Value,
            //         Name:         p.Name,
            //         Description:  p.Description,
            //         Price:        p.Price,
            //         OldPrice:     p.OldPrice,
            //         CategoryId:   p.CategoryId.Value,
            //         CategoryName: p.Category?.Name,
            //         StockQuantity:p.StockQuantity,
            //         Sold:         p.Sold,
            //         IsActive:     p.IsActive,
            //         Popular:      p.Popular,
            //         CreatedAt:    p.CreatedAt,
            //         UpdatedAt:    p.UpdatedAt,
            //         Images:       imageUrls,
            //         Colors:       colorOutputs,
            //         Reviews:      reviewOutputs
            //     ));
            // }
            //
            // // 4) Commit e retorno
            // await _unitOfWork.Commit();
            // return Result<GetAllProductItemOutput, Notification>.Ok(new GetAllProductItemOutput(items));
        }
    }
}


