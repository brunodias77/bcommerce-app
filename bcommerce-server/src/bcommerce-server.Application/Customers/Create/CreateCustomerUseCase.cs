using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.Domain.Validations;
using bcommerce_server.Infra.Repositories;

namespace bcommerce_server.Application.Customers.Create;

public class CreateCustomerUseCase : ICreateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerUseCase(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
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
        
        // ✅ Verifica se já existe um cliente com o mesmo e-mail
        // var existing = await _customerRepository.GetByEmail(customer.Email, CancellationToken.None);
        // if (existing != null)
        // {
        //     notification.Append(new Error("Já existe um cliente com este email."));
        //     return Result<CreateCustomerOutput, Notification>.Fail(notification);
        // }

        return await Create(customer);
    }

    private async Task<Result<CreateCustomerOutput, Notification>> Create(Customer customer)
    {
        var began = false;

        try
        {
            await _unitOfWork.Begin();
            began = true;

            await _customerRepository.Insert(customer, CancellationToken.None);
            await _unitOfWork.Commit();

            return Result<CreateCustomerOutput, Notification>.Ok(CreateCustomerOutput.FromCustomer(customer));
        }
        catch (Exception ex)
        {
            if (began)
                await _unitOfWork.Rollback();

            return Result<CreateCustomerOutput, Notification>.Fail(Notification.Create(new Error(ex.Message)));
        }
        finally
        {
            await _unitOfWork.DisposeAsync(); // garante que conexão/transação sejam liberadas
        }
    }
}