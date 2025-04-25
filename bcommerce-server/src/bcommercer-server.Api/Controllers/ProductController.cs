using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace bcommercer_server.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        // private readonly ICreateProductUseCase _createProduct;
        // private readonly IUpdateProductUseCase _updateProduct;
        // private readonly IGetProductUseCase _getProduct;
        // private readonly IDeleteProductUseCase _deleteProduct;

        // public ProductController(
        //     ICreateProductUseCase createProduct,
        //     IUpdateProductUseCase updateProduct,
        //     IGetProductUseCase getProduct,
        //     IDeleteProductUseCase deleteProduct)
        // {
        //     _createProduct = createProduct;
        //     _updateProduct = updateProduct;
        //     _getProduct = getProduct;
        //     _deleteProduct = deleteProduct;
        // }

        // [HttpPost]
        // [ProducesResponseType(typeof(CreateProductOutput), StatusCodes.Status201Created)]
        // [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
        // public async Task<IActionResult> Post([FromBody] CreateProductInput input)
        // {
        //     var result = await _createProduct.Execute(input);

        //     return result.Match<IActionResult>(
        //         success => CreatedAtAction(nameof(GetById), new { id = success.Id }, success),
        //         error => BadRequest(error.GetErrors())
        //     );
        // }

        // [HttpPut("{id:guid}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
        // public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProductInput input)
        // {
        //     if (id != input.Id)
        //         return BadRequest(new List<Error> { new Error("'id' da rota deve ser igual ao 'id' do corpo.") });

        //     var result = await _updateProduct.Execute(input);

        //     return result.Match<IActionResult>(
        //         _ => NoContent(),
        //         error => BadRequest(error.GetErrors())
        //     );
        // }

        // [HttpGet("{id:guid}")]
        // [ProducesResponseType(typeof(GetProductOutput), StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(List<Error>), StatusCodes.Status404NotFound)]
        // public async Task<IActionResult> GetById(Guid id)
        // {
        //     var result = await _getProduct.Execute(id);

        //     return result.Match<IActionResult>(
        //         success => Ok(success),
        //         error => NotFound(error.GetErrors())
        //     );
        // }

        // [HttpDelete("{id:guid}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
        // public async Task<IActionResult> Delete(Guid id)
        // {
        //     var result = await _deleteProduct.Execute(id);

        //     return result.Match<IActionResult>(
        //         _ => NoContent(),
        //         error => BadRequest(error.GetErrors())
        //     );
        // }
    }
}