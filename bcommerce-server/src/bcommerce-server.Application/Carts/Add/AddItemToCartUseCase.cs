using System;
using System.Threading;
using System.Threading.Tasks;
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Carts;
using bcommerce_server.Domain.Carts.Entities;
using bcommerce_server.Domain.Carts.Repositories;
using bcommerce_server.Domain.Products.Repostories;
using bcommerce_server.Domain.Services;
using bcommerce_server.Domain.Validations;
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
     var notification = Notification.Create();

        try
        {
            // 🔐 1. Obter cliente logado e validar
            var customer = await _loggedCustomer.User();
            customer.Validate(notification);
            if (notification.HasError())
                return Result<AddItemToCartOutput, Notification>.Fail(notification);

            // 💾 2. Iniciar transação (antes de qualquer SELECT/INSERT/UPDATE)
            if (!_unitOfWork.HasActiveTransaction)
                await _unitOfWork.Begin();
            
            // 🔍 3. Verificar se o produto existe
            var product = await _productRepository.Get(input.ProductId, CancellationToken.None);
            if (product is null)
            {
                notification.Append(new Error("Produto não encontrado."));
                await _unitOfWork.Rollback();
                return Result<AddItemToCartOutput, Notification>.Fail(notification);
            }

            // 🛒 4. Buscar ou criar carrinho do cliente
            var existingCart = await _cartRepository.GetByCustomerId(customer.Id.Value, CancellationToken.None);
            var cart = existingCart ?? Cart.NewCart(customer.Id.Value);

            // ➕ 5. Criar e validar item
            var item = CartItem.NewCartItem(input.ProductId,Guid.NewGuid(), 100, input.Quantity);
            item.Validate(notification);
            if (notification.HasError())
            {
                await _unitOfWork.Rollback();
                return Result<AddItemToCartOutput, Notification>.Fail(notification);
            }

            // 🧱 6. Adicionar item e validar carrinho
            cart.AddItem(item);
            cart.Validate(notification);
            if (notification.HasError())
            {
                await _unitOfWork.Rollback();
                return Result<AddItemToCartOutput, Notification>.Fail(notification);
            }

            // 💽 7. Persistir carrinho
            if (existingCart is null)
                await _cartRepository.Insert(cart, CancellationToken.None);
            else
                await _cartRepository.Update(cart, CancellationToken.None);

            // ✅ 8. Commit
            await _unitOfWork.Commit();

            // var output = new AddItemToCartOutput(cart.Id.Value);
            // return Result<AddItemToCartOutput, Notification>.Ok(output);
            // montar o carrinho completo com todos os products color e prices
            // Ter acesso ao cartID
            
            // pegar todos os cartItems que tem relacao com o nosso cartID
            
            // com cada cartID pegar seu respectivo product para ter acesso a outras informacoes
            // monstar o DTO e retornar
            
            return null;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            return Result<AddItemToCartOutput, Notification>.Fail(Notification.Create(ex));
        }
    }
}