using System.Threading.Tasks;
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Carts.Repositories;
using bcommerce_server.Domain.Products.Repostories;
using bcommerce_server.Domain.Services;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.Infra.Repositories;

namespace bcommerce_server.Application.Carts.Add;

public class AddItemToCartUseCase : IAddItemToCartUseCase
{
    public AddItemToCartUseCase(ICartRepository cartRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, ILoggedCustomer loggedCustomer)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _loggedCustomer = loggedCustomer;
    }

    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedCustomer _loggedCustomer;


    public async Task<Result<AddItemToCartOutput, Notification>> Execute(AddItemToCartInput input)
    {
        // cart e cart_items
        var notification = Notification.Create();
        // vou receber um { productID: {color: 1}}
        // pegar o usuario
        var customer = _loggedCustomer.User();
        // verificar se o carrinho desse usuario existe 
        //var cart = await _cartRepository.Get()
        // verificar se o produto existe 
        // validar tudo Cart.Validate(notification), CartItem.Validate(notification);
        //
        return  Task.FromResult(
            Result<AddItemToCartOutput, Notification>.Ok(new AddItemToCartOutput())
        );

    }
}