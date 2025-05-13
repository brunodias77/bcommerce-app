using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Validations.Handlers;

namespace bcommerce_server.Application.Carts.Add;

public interface IAddItemToCartUseCase : IUseCase<AddItemToCartInput, AddItemToCartOutput, Notification>
{
    
}