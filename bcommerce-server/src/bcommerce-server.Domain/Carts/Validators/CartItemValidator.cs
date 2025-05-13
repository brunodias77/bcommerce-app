// Domain/Carts/Validators/CartItemValidator.cs
using bcommerce_server.Domain.Carts.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Carts.Validators;

public class CartItemValidator : Validator
{
    private readonly CartItem _cartItem;

    public CartItemValidator(CartItem cartItem, IValidationHandler handler)
        : base(handler)
    {
        _cartItem = cartItem ?? throw new ArgumentNullException(nameof(cartItem));
    }

    public override void Validate()
    {
        ValidateProductId();
        ValidateQuantity();
        ValidateUnitPrice();
        ValidateColorId();
    }

    private void ValidateProductId()
    {
        if (_cartItem.ProductId == Guid.Empty)
            AddError("'productId' não pode ser vazio.");
    }

    private void ValidateQuantity()
    {
        if (_cartItem.Quantity <= 0)
            AddError("'quantity' deve ser maior que 0.");
    }

    private void ValidateUnitPrice()
    {
        if (_cartItem.UnitPrice <= 0)
            AddError("'unitPrice' deve ser maior que 0.");
    }

    private void ValidateColorId()
    {
        // Se você quiser exigir sempre cor, remova o primeiro teste
        if (_cartItem.ColorId.HasValue && _cartItem.ColorId == Guid.Empty)
            AddError("'colorId', se informado, deve ser um UUID válido.");
    }

    private void AddError(string message)
        => ValidationHandler.Append(new Error(message));
}



// using bcommerce_server.Domain.Carts.Entities;
// using bcommerce_server.Domain.Validations;
//
// namespace bcommerce_server.Domain.Carts.Validators;
//
// public class CartItemValidator : Validator
// {
//     private readonly CartItem _cartItem;
//
//     public CartItemValidator(CartItem cartItem, IValidationHandler handler)
//         : base(handler)
//     {
//         _cartItem = cartItem ?? throw new ArgumentNullException(nameof(cartItem));
//     }
//
//     public override void Validate()
//     {
//         ValidateProductId();
//         ValidateQuantity();
//     }
//
//     private void ValidateProductId()
//     {
//         if (_cartItem.ProductId == Guid.Empty)
//         {
//             AddError("'productId' não pode ser vazio.");
//         }
//     }
//
//     private void ValidateQuantity()
//     {
//         if (_cartItem.Quantity <= 0)
//         {
//             AddError("'quantity' deve ser maior que 0.");
//         }
//     }
//
//     private void AddError(string message)
//     {
//         ValidationHandler.Append(new Error(message));
//     }
// }
