using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Validations.Handlers;

namespace bcommerce_server.Application.Customers.Create;

public interface ICreateCustomerUseCase
    : IUseCase<CreateCustomerInput, CreateCustomerOutput, Notification>;
