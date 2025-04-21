using bcommerce_server.Application.Customers.Create;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using bcommerce_server.Domain.Validations;

namespace bcommercer_server.Api.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ICreateCustomerUseCase _createCustomer;

    public CustomerController(ICreateCustomerUseCase createCustomer)
    {
        _createCustomer = createCustomer;
    }
    
    [HttpPost("signup")]
    [ProducesResponseType(typeof(CreateCustomerOutput), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateCustomerInput input)
    {
        var result = await _createCustomer.Execute(input);

        return result.Match<IActionResult>(
            success => CreatedAtAction(nameof(Post), new { id = success.Id }, success),
            error => BadRequest(error.GetErrors())
        );
    }
}