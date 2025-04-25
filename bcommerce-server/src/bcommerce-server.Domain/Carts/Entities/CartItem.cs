using bcommerce_server.Domain.Carts.Identifiers;
using bcommerce_server.Domain.Carts.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Carts.Entities;

public class CartItem : Entity<CartItemID>
{
    private Guid _productId;
    private int _quantity;
    private DateTime _addedAt;

    private CartItem(
        CartItemID id,
        Guid productId,
        int quantity,
        DateTime addedAt
    ) : base(id)
    {
        _productId = productId;
        _quantity = quantity;
        _addedAt = addedAt;
    }

    public static CartItem Create(Guid productId, int quantity)
    {
        return new CartItem(
            CartItemID.Generate(),
            productId,
            quantity,
            DateTime.UtcNow
        );
    }

    public static CartItem With(CartItemID id, Guid productId, int quantity, DateTime addedAt)
    {
        return new CartItem(id, productId, quantity, addedAt);
    }

    public CartItem UpdateQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new CartItemValidator(this, handler).Validate();
    }

    // Getters públicos
    public Guid ProductId => _productId;
    public int Quantity => _quantity;
    public DateTime AddedAt => _addedAt;
}
