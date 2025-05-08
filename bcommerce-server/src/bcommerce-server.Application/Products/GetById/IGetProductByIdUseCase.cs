using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Validations.Handlers;

namespace bcommerce_server.Application.Products.GetById;

public interface IGetProductByIdUseCase :  IUseCase<GetProductByIdInput, GetProductByIdOutput, Notification>
{
    
}