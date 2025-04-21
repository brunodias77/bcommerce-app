using bcommerce_server.Application.Customers.Create;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomerInput command)
    {
        var result = await _createCustomer.Execute(command);

        return result.Match<IActionResult>(
            success => Ok(success),
            error => BadRequest(error.GetErrors())
        );
    }
}