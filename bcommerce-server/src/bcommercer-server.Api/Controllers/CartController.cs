using bcommerce_server.Application.Carts.Add;
using bcommerce_server.Domain.Validations.Handlers;
using bcommercer_server.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace bcommercer_server.Api.Controllers;

[ApiController]
[Route("api/carts")]
public class CartController: ControllerBase
{   
    
    
    [AuthenticatedUser]
    [HttpPost("add-item")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Notification), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddItemToCart(
        [FromBody] AddItemToCartInput input,
        [FromServices] IAddItemToCartUseCase useCase, 
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(input);
        return Ok();
    }
}