using bcommerce_server.Domain.Carts.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Carts.Entities;

public sealed class CartItem : ValueObject
{
    private readonly Guid _productId;
    private readonly Guid? _colorId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
    private readonly DateTime _addedAt;

    private CartItem(
        Guid productId,
        Guid? colorId,
        decimal unitPrice,
        int quantity,
        DateTime addedAt
    )
    {
        _productId = productId;
        _colorId = colorId;
        _unitPrice = unitPrice;
        _quantity = quantity;
        _addedAt = addedAt;
    }

    public static CartItem NewCartItem(Guid productId, Guid? colorId, decimal unitPrice, int quantity)
    {
        return new CartItem(
            productId,
            colorId,
            unitPrice,
            quantity,
            DateTime.UtcNow
        );
    }

    public static CartItem With(
        Guid productId,
        Guid? colorId,
        decimal unitPrice,
        int quantity,
        DateTime addedAt
    )
    {
        return new CartItem(productId, colorId, unitPrice, quantity, addedAt);
    }

    public CartItem UpdateQuantity(int quantity)
    {
        return new CartItem(_productId, _colorId, _unitPrice, quantity, _addedAt);
    }

    public void Validate(IValidationHandler handler)
    {
        new CartItemValidator(this, handler).Validate();
    }

    // Propriedades públicas (somente leitura)
    public Guid ProductId    => _productId;
    public Guid? ColorId     => _colorId;
    public decimal UnitPrice => _unitPrice;
    public int Quantity      => _quantity;
    public DateTime AddedAt  => _addedAt;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _productId;
        yield return _colorId ?? Guid.Empty;
        yield return _unitPrice;
        yield return _quantity;
        yield return _addedAt;
    }
}


// // Domain/Carts/Entities/CartItem.cs
// using bcommerce_server.Domain.Carts.Identifiers;
// using bcommerce_server.Domain.Carts.Validators;
// using bcommerce_server.Domain.SeedWork;
// using bcommerce_server.Domain.Validations;
//
// namespace bcommerce_server.Domain.Carts.Entities;
//
// public class CartItem : Entity<CartItemID>
// {
//     private Guid _productId;
//     private Guid? _colorId;
//     private decimal _unitPrice;
//     private int _quantity;
//     private DateTime _addedAt;
//
//     private CartItem(
//         CartItemID id,
//         Guid productId,
//         Guid? colorId,
//         decimal unitPrice,
//         int quantity,
//         DateTime addedAt
//     ) : base(id)
//     {
//         _productId = productId;
//         _colorId = colorId;
//         _unitPrice = unitPrice;
//         _quantity = quantity;
//         _addedAt = addedAt;
//     }
//
//     public static CartItem NewCartItem(Guid productId, Guid? colorId, decimal unitPrice, int quantity)
//     {
//         return new CartItem(
//             CartItemID.Generate(),
//             productId,
//             colorId,
//             unitPrice,
//             quantity,
//             DateTime.UtcNow
//         );
//     }
//
//     public static CartItem With(
//         CartItemID id,
//         Guid productId,
//         Guid? colorId,
//         decimal unitPrice,
//         int quantity,
//         DateTime addedAt
//     ) => new CartItem(id, productId, colorId, unitPrice, quantity, addedAt);
//
//     public CartItem UpdateQuantity(int quantity)
//     {
//         _quantity = quantity;
//         return this;
//     }
//
//     public override void Validate(IValidationHandler handler)
//     {
//         new CartItemValidator(this, handler).Validate();
//     }
//
//     // getters
//     public Guid ProductId   => _productId;
//     public Guid? ColorId    => _colorId;
//     public decimal UnitPrice=> _unitPrice;
//     public int Quantity     => _quantity;
//     public DateTime AddedAt => _addedAt;
// }
//
