using bcommerce_server.Domain.Carts.Entities;
using bcommerce_server.Domain.Carts.Identifiers;
using bcommerce_server.Domain.Carts.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Carts;

/// <summary>
/// Raiz de agregação que representa um carrinho de compras.
/// </summary>
public class Cart : AggregateRoot<CartID>
{
    private Guid _customerId;
    private List<CartItem> _items;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    private Cart(
        CartID id,
        Guid customerId,
        List<CartItem> items,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _customerId = customerId;
        _items = items ?? new List<CartItem>();
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public static Cart NewCart(Guid customerId)
    {
        var now = DateTime.UtcNow;
        return new Cart(
            CartID.Generate(),
            customerId,
            new List<CartItem>(),
            now,
            now
        );
    }

    public static Cart With(
        CartID id,
        Guid customerId,
        List<CartItem> items,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new Cart(id, customerId, items, createdAt, updatedAt);
    }

    public Cart AddItem(CartItem item)
    {
        _items.Add(item);
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Cart RemoveItem(CartItem item)
    {
        _items.Remove(item);
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Cart Clear()
    {
        _items.Clear();
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new CartValidator(this, handler).Validate();
    }

    // Propriedades públicas

    public Guid CustomerId => _customerId;

    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;
}