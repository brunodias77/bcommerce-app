using bcommerce_server.Application.Customers.Create;
using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Repostories;
using Microsoft.AspNetCore.Mvc;

namespace bcommercer_server.Api.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController :  ControllerBase
{
    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    private readonly IProductRepository _repository;
    // [HttpGet]
    // [ProducesResponseType(typeof(List<ProductOutput>), StatusCodes.Status200OK)]
    // [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    
    [HttpGet]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var products = await _repository.GetAllProducts(cancellationToken);
            return Ok(products);
        }
        catch (Exception ex)
        {
            // 🔍 Log o erro aqui se quiser (ex: _logger.LogError(ex, ...))
            return StatusCode(500, new { error = "Erro ao buscar produtos", details = ex.Message });
        }
    }
}