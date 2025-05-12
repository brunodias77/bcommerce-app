// Domain/Products/Entities/ProductColor.cs
using System;
using bcommerce_server.Domain.Products.Identifiers;
using bcommerce_server.Domain.Products.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Entities
{
    public class ProductColor : Entity<ProductColorID>
    {
        private Guid _productId;
        private Guid _colorId;
        private DateTime _createdAt;
        private DateTime _updatedAt;

        private ProductColor(
            ProductColorID id,
            Guid productId,
            Guid colorId,
            DateTime createdAt,
            DateTime updatedAt
        ) : base(id)
        {
            _productId = productId;
            _colorId = colorId;
            _createdAt = createdAt;
            _updatedAt = updatedAt;
        }

        public static ProductColor NewProductColor(Guid productId, Guid colorId)
        {
            var now = DateTime.UtcNow;
            return new ProductColor(
                ProductColorID.Generate(),
                productId,
                colorId,
                now,
                now
            );
        }

        public static ProductColor With(
            ProductColorID id,
            Guid productId,
            Guid colorId,
            DateTime createdAt,
            DateTime updatedAt
        ) => new ProductColor(id, productId, colorId, createdAt, updatedAt);

        public override void Validate(IValidationHandler handler)
        {
            new ProductColorValidator(this, handler).Validate();
        }

        // Getters
        public Guid ProductId => _productId;
        public Guid ColorId   => _colorId;
        public DateTime CreatedAt => _createdAt;
        public DateTime UpdatedAt => _updatedAt;
    }
}




// using bcommerce_server.Domain.Products.Identifiers;
// using bcommerce_server.Domain.Products.Validators;
// using bcommerce_server.Domain.Products.ValueObjects;
// using bcommerce_server.Domain.SeedWork;
// using bcommerce_server.Domain.Validations;
//
// namespace bcommerce_server.Domain.Products.Entities;
//
// public class ProductColor : Entity<ProductColorID>
// {
//     private ColorValue _color;
//     private DateTime _createdAt;
//
//     private ProductColor(ProductColorID id, ColorValue color, DateTime createdAt)
//         : base(id)
//     {
//         _color = color;
//         _createdAt = createdAt;
//     }
//
//     public static ProductColor Create(ColorValue color)
//     {
//         return new ProductColor(ProductColorID.Generate(), color, DateTime.UtcNow);
//     }
//
//     public static ProductColor With(ProductColorID id, ColorValue color, DateTime createdAt)
//     {
//         return new ProductColor(id, color, createdAt);
//     }
//
//     public override void Validate(IValidationHandler handler)
//     {
//         new ProductColorValidator(this, handler).Validate();
//     }
//
//     public ColorValue Color => _color;
//     public DateTime CreatedAt => _createdAt;
// }