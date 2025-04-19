using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.Validations.Handlers;

namespace bcommerce_server.Application.Customers.Create;

public class CreateCustomerUseCase : ICreateCustomerUseCase
{
    private readonly ICustomerRepository _repository;

    public CreateCustomerUseCase(ICustomerRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<CreateCustomerOutput, Notification>> Execute(CreateCustomerInput input)
    {
        var notification = Notification.Create();

        var email = Email.From(input.Email);
        var cpf = Cpf.From(input.Cpf);

        var customer = Customer.NewCustomer(input.Name, email!, cpf!);
        customer.Validate(notification);

        if (notification.HasError())
            return Result<CreateCustomerOutput, Notification>.Fail(notification);

        try
        {
            await _repository.Insert(customer, CancellationToken.None);
            return Result<CreateCustomerOutput, Notification>.Ok(CreateCustomerOutput.From(customer));
        }
        catch (Exception ex)
        {
            return Result<CreateCustomerOutput, Notification>.Fail(Notification.Create(ex));
        }
    }
}