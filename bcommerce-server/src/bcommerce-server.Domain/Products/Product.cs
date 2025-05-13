using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;
using System.Collections.ObjectModel;

namespace bcommerce_server.Domain.Products;

/// <summary>
/// Raiz de agregação que representa um produto do catálogo.
/// </summary>
public class Product : AggregateRoot<ProductID>
{
    private string _name;
    private string _description;
    private decimal _price;
    private decimal? _oldPrice;
    private CategoryID _categoryId;
    private Category? _category; // ✅ Referência completa à entidade Category
    private int _stockQuantity;
    private int _sold;
    private bool _isActive;
    private bool _popular;
    private List<ProductImage> _images;
    private List<ProductColor> _colors;
    private List<ProductReview> _reviews;
    private DateTime? _deletedAt;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    // ✅ Construtor privado com categoria opcional
    private Product(
        ProductID id,
        string name,
        string description,
        decimal price,
        decimal? oldPrice,
        CategoryID categoryId,
        Category? category, // ✅
        int stockQuantity,
        int sold,
        bool isActive,
        bool popular,
        List<ProductImage> images,
        List<ProductColor> colors,
        List<ProductReview> reviews,
        DateTime? deletedAt,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _name = name;
        _description = description;
        _price = price;
        _oldPrice = oldPrice;
        _categoryId = categoryId;
        _category = category; // ✅
        _stockQuantity = stockQuantity;
        _sold = sold;
        _isActive = isActive;
        _popular = popular;
        _images = images ?? new();
        _colors = colors ?? new();
        _reviews = reviews ?? new();
        _deletedAt = deletedAt;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    // ✅ Método de criação simples, sem Category carregada
    public static Product NewProduct(
        string name,
        string description,
        decimal price,
        CategoryID categoryId
    )
    {
        var now = DateTime.UtcNow;
        return new Product(
            ProductID.Generate(),
            name,
            description,
            price,
            null,
            categoryId,
            null, // ✅ Category ainda não carregada
            0,
            0,
            true,
            false,
            new List<ProductImage>(),
            new List<ProductColor>(),
            new List<ProductReview>(),
            null,
            now,
            now
        );
    }

    // ✅ Método With com Category opcional
    public static Product With(
        ProductID id,
        string name,
        string description,
        decimal price,
        decimal? oldPrice,
        CategoryID categoryId,
        int stockQuantity,
        int sold,
        bool isActive,
        bool popular,
        List<ProductImage> images,
        List<ProductColor> colors,
        List<ProductReview> reviews,
        DateTime? deletedAt,
        DateTime createdAt,
        DateTime updatedAt,
        Category? category = null // ✅ permite enriquecer a entidade com dados da Category
    )
    {
        return new Product(id, name, description, price, oldPrice, categoryId, category, stockQuantity, sold, isActive, popular, images, colors, reviews, deletedAt, createdAt, updatedAt);
    }

    public Product Update(
        string name,
        string description,
        decimal price,
        decimal? oldPrice,
        int stockQuantity,
        bool popular
    )
    {
        _name = name;
        _description = description;
        _price = price;
        _oldPrice = oldPrice;
        _stockQuantity = stockQuantity;
        _popular = popular;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Product AddImage(ProductImage image)
    {
        _images.Add(image);
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Product AddColor(ProductColor color)
    {
        _colors.Add(color);
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Product AddReview(ProductReview review)
    {
        _reviews.Add(review);
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Product Deactivate()
    {
        _isActive = false;
        _deletedAt = DateTime.UtcNow;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Product Activate()
    {
        _isActive = true;
        _deletedAt = null;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new ProductValidator(this, handler).Validate();
    }

    // ✅ Propriedades públicas
    public string Name => _name;
    public string Description => _description;
    public decimal Price => _price;
    public decimal? OldPrice => _oldPrice;
    public CategoryID CategoryId => _categoryId;
    public Category? Category => _category; // ✅ nova propriedade pública

    public int StockQuantity => _stockQuantity;
    public int Sold => _sold;
    public bool IsActive => _isActive;
    public bool Popular => _popular;
    public DateTime? DeletedAt => _deletedAt;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;

    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();
    public IReadOnlyCollection<ProductColor> Colors => _colors.AsReadOnly();
    public IReadOnlyCollection<ProductReview> Reviews => _reviews.AsReadOnly();

    public object Clone()
    {
        return With(
            Id,
            Name,
            Description,
            Price,
            OldPrice,
            CategoryId,
            StockQuantity,
            Sold,
            IsActive,
            Popular,
            _images.ToList(),
            _colors.ToList(),
            _reviews.ToList(),
            DeletedAt,
            CreatedAt,
            UpdatedAt,
            _category // ✅ inclui Category no clone
        );
    }
}

