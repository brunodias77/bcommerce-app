using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Application.Customers.Create;

public class CreateCustomerUseCase : ICreateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<CreateCustomerOutput, Notification>> Execute(CreateCustomerInput input)
    {
        var notification = Notification.Create();

        var customer = Customer.NewCustomer(
            input.Name,
            Email.From(input.Email), 
            input.Password
        );

        customer.Validate(notification);

        if (notification.HasError())
            return Result<CreateCustomerOutput, Notification>.Fail(notification);

        return await Create(customer);
    }

    private async Task<Result<CreateCustomerOutput, Notification>> Create(Customer customer)
    {
        try
        {
            await _customerRepository.Insert(customer, CancellationToken.None);
            return Result<CreateCustomerOutput, Notification>.Ok(CreateCustomerOutput.FromCustomer(customer));
        }
        catch (Exception ex)
        {
            return Result<CreateCustomerOutput, Notification>.Fail(Notification.Create(new Error(ex.Message)));
        }
    }
}