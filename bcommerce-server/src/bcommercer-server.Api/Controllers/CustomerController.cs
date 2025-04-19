using bcommerce_server.Application.Customers.Create;
using bcommerce_server.Domain.Addresses;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Customers.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace bcommercer_server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICreateCustomerUseCase _createCustomerUseCase;

    public CustomersController(ICreateCustomerUseCase createCustomerUseCase)
    {
        _createCustomerUseCase = createCustomerUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerInput input)
    {
        var result = await _createCustomerUseCase.Execute(input);

        return result.Match<IActionResult>(
            success => Ok(success),
            error => BadRequest(new
            {
                Errors = error.GetErrors().Select(e => e.Message)
            })
        );
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        // Placeholder. Implementar o GetByIdUseCase depois.
        return Ok(new { Message = $"Customer {id} would be retrieved here." });
    }
    
    
    
    // private readonly ICustomerRepository _repository;
    //
    // public CustomersController(ICustomerRepository repository)
    // {
    //     _repository = repository;
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> Create([FromBody] CreateCustomerInput request, CancellationToken cancellationToken)
    // {
    //     var email = Email.From(request.Email);
    //     var cpf = Cpf.From(request.Cpf);
    //
    //     var customer = Customer.NewCustomer(request.Name, email, cpf);
    //
    //     foreach (var address in request.Addresses)
    //     {
    //         customer.AddAddress(Address.NewAddress(
    //             customer.Id,
    //             address.Street,
    //             address.Number,
    //             address.City,
    //             address.State,
    //             address.ZipCode
    //         ));
    //     }
    //
    //     await _repository.Insert(customer, cancellationToken);
    //
    //     return CreatedAtAction(nameof(GetById), new { id = customer.Id.Value }, customer.Id);
    // }
    //
    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    // {
    //     var customer = await _repository.Get(id, cancellationToken);
    //
    //     if (customer is null)
    //         return NotFound();
    //
    //     return Ok(new
    //     {
    //         customer.Id,
    //         customer.Name,
    //         Email = customer.Email.ToString(),
    //         Cpf = customer.Cpf.Number,
    //         customer.IsDeleted,
    //         customer.CreatedAt,
    //         customer.UpdatedAt,
    //         Addresses = customer.Addresses.Select(a => new
    //         {
    //             a.Id,
    //             a.Street,
    //             a.Number,
    //             a.City,
    //             a.State,
    //             a.ZipCode,
    //             a.CreatedAt
    //         })
    //     });
    // }
}