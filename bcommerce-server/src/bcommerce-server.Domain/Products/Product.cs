using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.Validators;
using bcommerce_server.Domain.Products.ValueObjects;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products;

public class Product : AggregateRoot<ProductID>
{
    private string _name;
    private string _description;
    private Price _price;
    private Price? _oldPrice;
    private List<ImageUrl>? _images;
    private Category _category;
    private List<Color> _colors;
    private Stock _stock;
    private int _sold;
    private bool _isActive;
    private bool _popular;
    private DateTime _createdAt;
    private DateTime _updatedAt; // 🆕 Novo campo

    private Product(
        ProductID id,
        string name,
        string description,
        Price price,
        Price? oldPrice,
        List<ImageUrl>? images,
        Category category,
        List<Color> colors,
        Stock stock,
        int sold,
        bool isActive,
        bool popular,
        DateTime createdAt,
        DateTime updatedAt // 🆕 Novo parâmetro
    ) : base(id)
    {
        _name = name;
        _description = description;
        _price = price;
        _oldPrice = oldPrice;
        _images = images;
        _category = category;
        _colors = colors;
        _stock = stock;
        _sold = sold;
        _isActive = isActive;
        _popular = popular;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public static Product NewProduct(
        string name,
        string description,
        Price price,
        List<ImageUrl>? images,
        Category category,
        List<Color> colors,
        Stock stock,
        bool isActive = true,
        bool popular = false
    )
    {
        var now = DateTime.UtcNow;
        return new Product(
            ProductID.Generate(),
            name,
            description,
            price,
            null,
            images,
            category,
            colors,
            stock,
            0,
            isActive,
            popular,
            now,
            now
        );
    }

    public static Product With(
        ProductID id,
        string name,
        string description,
        Price price,
        Price? oldPrice,
        List<ImageUrl>? images,
        Category category,
        List<Color> colors,
        Stock stock,
        int sold,
        bool isActive,
        bool popular,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new Product(id, name, description, price, oldPrice, images, category, colors, stock, sold, isActive, popular, createdAt, updatedAt);
    }

    public Product Update(
        string name,
        string description,
        Price price,
        Price? oldPrice,
        List<ImageUrl>? images,
        Category category,
        List<Color> colors,
        Stock stock,
        int sold,
        bool isActive,
        bool popular
    )
    {
        _name = name;
        _description = description;
        _price = price;
        _oldPrice = oldPrice;
        _images = images;
        _category = category;
        _colors = colors;
        _stock = stock;
        _sold = sold;
        _isActive = isActive;
        _popular = popular;
        _updatedAt = DateTime.UtcNow; // 🆕 Atualiza modificação

        return this;
    }

    // 🆕 Ativar o produto
    public void Activate()
    {
        if (!_isActive)
        {
            _isActive = true;
            _updatedAt = DateTime.UtcNow;
        }
    }

    // 🆕 Desativar o produto
    public void Deactivate()
    {
        if (_isActive)
        {
            _isActive = false;
            _updatedAt = DateTime.UtcNow;
        }
    }

    public override void Validate(IValidationHandler handler)
    {
        new ProductValidator(this, handler).Validate();
    }

    // Propriedades públicas

    public string Name => _name;
    public string Description => _description;
    public Price Price => _price;
    public Price? OldPrice => _oldPrice;
    public IReadOnlyCollection<ImageUrl> Images => (IReadOnlyCollection<ImageUrl>?)_images?.AsReadOnly() ?? Array.Empty<ImageUrl>();
    public Category Category => _category;
    public IReadOnlyCollection<Color> Colors => _colors.AsReadOnly();
    public Stock Stock => _stock;
    public int Sold => _sold; // Vendidos
    public bool IsActive => _isActive;
    public bool Popular => _popular;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt; // 🆕

    // Propriedades derivadas
    public bool IsNew => _createdAt >= DateTime.UtcNow.AddDays(-30); // Novidade
    public bool Sale => _oldPrice is not null && _oldPrice.Amount > _price.Amount; // Promocao

    public object Clone()
    {
        return With(
            Id,
            Name,
            Description,
            Price,
            OldPrice,
            _images.ToList(),
            Category,
            _colors.ToList(),
            Stock,
            Sold,
            IsActive,
            Popular,
            CreatedAt,
            UpdatedAt
        );
    }
}



