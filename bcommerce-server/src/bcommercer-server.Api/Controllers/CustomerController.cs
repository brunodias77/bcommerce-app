using bcommerce_server.Application.Customers.Create;
using bcommerce_server.Application.Customers.Login;
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

        if (result.IsSuccess)
            return CreatedAtAction(nameof(Post), new { id = result.Value!.Id }, result.Value);

        return BadRequest(result.Error!.GetErrors());
    }

    [HttpPost("signin")]
    [ProducesResponseType(typeof(LoginCustomerOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginCustomerUseCase _loginCustomer,
        [FromBody] LoginCustomerInput input)
    {
        var result = await _loginCustomer.Execute(input);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error!.GetErrors());
    }
}




// using bcommerce_server.Application.Customers.Create;
// using bcommerce_server.Application.Customers.Login;
// using Microsoft.AspNetCore.Http.HttpResults;
// using Microsoft.AspNetCore.Mvc;
// using bcommerce_server.Domain.Validations;
// using bcommercer_server.Api.Attributes;
//
// namespace bcommercer_server.Api.Controllers;
//
// [ApiController]
// [Route("api/customer")]
// public class CustomerController : ControllerBase
// {
//     public CustomerController(ICreateCustomerUseCase createCustomer)
//     {
//         _createCustomer = createCustomer;
//     }
//
//     private readonly ICreateCustomerUseCase _createCustomer;
//
//     [HttpPost("signup")]
//     [ProducesResponseType(typeof(CreateCustomerOutput), StatusCodes.Status201Created)]
//     [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
//     public async Task<IActionResult> Post([FromBody] CreateCustomerInput input)
//     {
//         var result = await _createCustomer.Execute(input);
//
//         return result.Match<IActionResult>(
//             success => CreatedAtAction(nameof(Post), new { id = success.Id }, success),
//             error => BadRequest(error.GetErrors())
//         );
//     }
//     
//     [HttpPost("signin")]
//     [ProducesResponseType(typeof(LoginCustomerOutput), StatusCodes.Status200OK)]
//     [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
//     public async Task<IActionResult> Login([FromServices] ILoginCustomerUseCase _loginCustomer, [FromBody] LoginCustomerInput input)
//     {
//         var result = await _loginCustomer.Execute(input);
//
//         return result.Match<IActionResult>(
//             success => Ok(success),
//             error => BadRequest(error.GetErrors())
//         );
//     }
// }