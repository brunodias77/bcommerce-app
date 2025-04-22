using bcommerce_server.Application.Customers.Create;
using bcommerce_server.Application.Customers.Login;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using bcommerce_server.Domain.Validations;

namespace bcommercer_server.Api.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    public CustomerController(ICreateCustomerUseCase createCustomer, ILoginCustomerUseCase loginCustomer)
    {
        _createCustomer = createCustomer;
        _loginCustomer = loginCustomer;
    }

    private readonly ICreateCustomerUseCase _createCustomer;
    private readonly ILoginCustomerUseCase _loginCustomer;


    
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
    
    [HttpPost("signin")]
    [ProducesResponseType(typeof(LoginCustomerOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginCustomerInput input)
    {
        var result = await _loginCustomer.Execute(input);

        return result.Match<IActionResult>(
            success => Ok(success),
            error => BadRequest(error.GetErrors())
        );
    }
}