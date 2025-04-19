using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;
using bcommerce_server.Domain.Validations.Handlers;

namespace bcommerce_server.Domain.Categories;

    /// <summary>
    /// Representa a entidade de Categoria no domínio.
    /// Controla estado, validação e ciclo de vida da categoria.
    /// </summary>
    public class Category : AggregateRoot<CategoryID>
    {
        private string _name;
        private string _description;
        private bool _active;
        private DateTime _createdAt;
        private DateTime _updatedAt;
        private DateTime? _deletedAt;

        // Construtor privado
        private Category(CategoryID id, string name, string description, bool active,
                         DateTime createdAt, DateTime updatedAt, DateTime? deletedAt)
            : base(id)
        {
            _name = name;
            _description = description;
            _active = active;
            _createdAt = createdAt != default ? createdAt : throw new ArgumentNullException(nameof(createdAt));
            _updatedAt = updatedAt != default ? updatedAt : throw new ArgumentNullException(nameof(updatedAt));
            _deletedAt = deletedAt;
        }

        // Métodos de Fábrica
        public static Category NewCategory(string name, string description, bool active)
        {
            var id = CategoryID.Generate();
            var now = DateTime.UtcNow;
            DateTime? deletedAt = active ? null : now; 
            return new Category(id, name, description, active, now, now, deletedAt);
        }

        public static Category With(CategoryID id, string name, string description, bool active,
                                    DateTime createdAt, DateTime updatedAt, DateTime? deletedAt)
        {
            return new Category(id, name, description, active, createdAt, updatedAt, deletedAt);
        }

        public static Category With(Category category)
        {
            return new Category(
                category.Id,
                category._name,
                category._description,
                category._active,
                category._createdAt,
                category._updatedAt,
                category._deletedAt
            );
        }

        // Validação
        public override void Validate(IValidationHandler handler)
        {
            new CategoryValidator(this, handler).Validate();
        }

        // Regras de Negócio

        public Category Deactivate()
        {
            if (_deletedAt == null)
            {
                _deletedAt = DateTime.UtcNow;
            }
            _active = false;
            _updatedAt = DateTime.UtcNow;
            return this;
        }

        public Category Activate()
        {
            _deletedAt = null;
            _active = true;
            _updatedAt = DateTime.UtcNow;
            return this;
        }

        public Category Update(string name, string description, bool active)
        {
            if (active)
                Activate();
            else
                Deactivate();

            _name = name;
            _description = description;
            _updatedAt = DateTime.UtcNow;

            return this;
        }

        // Propriedades públicas

        public string Name => _name;
        public string Description => _description;
        public bool IsActive => _active;
        public DateTime CreatedAt => _createdAt;
        public DateTime UpdatedAt => _updatedAt;
        public DateTime? DeletedAt => _deletedAt;

        // Clone (defensive copy)
        public object Clone()
        {
            return With(this);
        }

  
    }