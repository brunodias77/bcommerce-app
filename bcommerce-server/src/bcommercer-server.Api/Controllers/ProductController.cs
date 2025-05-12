using bcommerce_server.Application.Abstractions;
using bcommerce_server.Application.Products.GetAll;
using bcommerce_server.Application.Products.GetById;
using bcommerce_server.Domain.Validations.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace bcommercer_server.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IGetAllProuctsUseCase _getAllProuctsUseCase;
        private readonly IGetProductByIdUseCase _getProductByIdUseCase;

        public ProductController(
            IGetAllProuctsUseCase getAllProuctsUseCase,
            IGetProductByIdUseCase getProductByIdUseCase)
        {
            _getAllProuctsUseCase = getAllProuctsUseCase;
            _getProductByIdUseCase = getProductByIdUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllProductsOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var result = await _getAllProuctsUseCase.Execute(Unit.Value);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetProductByIdOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductById([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _getProductByIdUseCase.Execute(new GetProductByIdInput(id));

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using bcommerce_server.Application.Abstractions;
// using bcommerce_server.Application.Products.GetAll;
// using bcommerce_server.Application.Products.GetById;
// using bcommerce_server.Domain.Validations.Handlers;
// using Microsoft.AspNetCore.Mvc;
//
// namespace bcommercer_server.Api.Controllers
// {
//     [ApiController]
//     [Route("api/products")]
//     public class ProductController : ControllerBase
//     {
//         public ProductController(IGetAllProuctsUseCase getAllProuctsUseCase, IGetProductByIdUseCase getProductByIdUseCase)
//         {
//             _getAllProuctsUseCase = getAllProuctsUseCase;
//             _getProductByIdUseCase = getProductByIdUseCase;
//         }
//
//         // private readonly ICreateProductUseCase _createProduct;
//         // private readonly IUpdateProductUseCase _updateProduct;
//         // private readonly IGetProductUseCase _getProduct;
//         // private readonly IDeleteProductUseCase _deleteProduct;
//         
//         private readonly IGetAllProuctsUseCase _getAllProuctsUseCase;
//         private readonly IGetProductByIdUseCase _getProductByIdUseCase;
//         
//         [HttpGet]
//         [ProducesResponseType(typeof(List<GetAllProductsOutput>), StatusCodes.Status200OK)]
//         [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
//         public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
//         {
//             
//             var result = await _getAllProuctsUseCase.Execute(Unit.Value);
//
//             return result.Match<IActionResult>(
//                 success => Ok(success),
//                 error => BadRequest(error)
//             );
//         }
//
//         [HttpGet("{id:guid}")]
//         [ProducesResponseType(typeof(List<GetProductByIdOutput>), StatusCodes.Status200OK)]
//         [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
//         public async Task<IActionResult> GetProductById([FromRoute] string id, CancellationToken cancellationToken)
//         {
//             var result = await _getProductByIdUseCase.Execute(new GetProductByIdInput(id));
//             return result.Match<IActionResult>(
//                 success => Ok(success),
//                 error => BadRequest(error)
//             );       
//         }
//
//         // public ProductController(
//         //     ICreateProductUseCase createProduct,
//         //     IUpdateProductUseCase updateProduct,
//         //     IGetProductUseCase getProduct,
//         //     IDeleteProductUseCase deleteProduct)
//         // {
//         //     _createProduct = createProduct;
//         //     _updateProduct = updateProduct;
//         //     _getProduct = getProduct;
//         //     _deleteProduct = deleteProduct;
//         // }
//         
//         
//
//         // [HttpPost]
//         // [ProducesResponseType(typeof(CreateProductOutput), StatusCodes.Status201Created)]
//         // [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
//         // public async Task<IActionResult> Post([FromBody] CreateProductInput input)
//         // {
//         //     var result = await _createProduct.Execute(input);
//
//         //     return result.Match<IActionResult>(
//         //         success => CreatedAtAction(nameof(GetById), new { id = success.Id }, success),
//         //         error => BadRequest(error.GetErrors())
//         //     );
//         // }
//
//         // [HttpPut("{id:guid}")]
//         // [ProducesResponseType(StatusCodes.Status204NoContent)]
//         // [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
//         // public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProductInput input)
//         // {
//         //     if (id != input.Id)
//         //         return BadRequest(new List<Error> { new Error("'id' da rota deve ser igual ao 'id' do corpo.") });
//
//         //     var result = await _updateProduct.Execute(input);
//
//         //     return result.Match<IActionResult>(
//         //         _ => NoContent(),
//         //         error => BadRequest(error.GetErrors())
//         //     );
//         // }
//
//         // [HttpGet("{id:guid}")]
//         // [ProducesResponseType(typeof(GetProductOutput), StatusCodes.Status200OK)]
//         // [ProducesResponseType(typeof(List<Error>), StatusCodes.Status404NotFound)]
//         // public async Task<IActionResult> GetById(Guid id)
//         // {
//         //     var result = await _getProduct.Execute(id);
//
//         //     return result.Match<IActionResult>(
//         //         success => Ok(success),
//         //         error => NotFound(error.GetErrors())
//         //     );
//         // }
//
//         // [HttpDelete("{id:guid}")]
//         // [ProducesResponseType(StatusCodes.Status204NoContent)]
//         // [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
//         // public async Task<IActionResult> Delete(Guid id)
//         // {
//         //     var result = await _deleteProduct.Execute(id);
//
//         //     return result.Match<IActionResult>(
//         //         _ => NoContent(),
//         //         error => BadRequest(error.GetErrors())
//         //     );
//         // }
//     }
// }