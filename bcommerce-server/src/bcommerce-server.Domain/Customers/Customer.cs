using bcommerce_server.Domain.Customers.Entities;
using bcommerce_server.Domain.Customers.Identifiers;
using bcommerce_server.Domain.Customers.Validators;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Customers;

/// <summary>
/// Raiz de agregação que representa um cliente do sistema.
/// </summary>
public class Customer : AggregateRoot<CustomerID>
{
    private string _name;
    private Email _email;
    private string _password;
    private Cpf? _cpf;
    private List<CustomerAddress>? _addresses;
    private DateTime? _deletedAt;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    private Customer(
        CustomerID id,
        string name,
        Email email,
        string password,
        Cpf? cpf,
        List<CustomerAddress>? addresses,
        DateTime? deletedAt,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _name = name;
        _email = email;
        _password = password;
        _cpf = cpf;
        _addresses = addresses;
        _deletedAt = deletedAt;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public static Customer NewCustomer(string name, Email email, string password)
    {
        var now = DateTime.UtcNow;
        return new Customer(
            CustomerID.Generate(),
            name,
            email,
            password,
            null,
            new List<CustomerAddress>(),
            null,
            now,
            now
        );
    }

    public static Customer With(
        CustomerID id,
        string name,
        Email email,
        string password,
        Cpf? cpf,
        List<CustomerAddress>? addresses,
        DateTime? deletedAt,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new Customer(id, name, email, password, cpf, addresses, deletedAt, createdAt, updatedAt);
    }

    public Customer Update(string name, Email email, string password, Cpf? cpf = null)
    {
        _name = name;
        _email = email;
        _password = password;
        _cpf = cpf ?? _cpf;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Customer AddAddress(CustomerAddress address)
    {
        _addresses ??= new List<CustomerAddress>();
        _addresses.Add(address);
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Customer Deactivate()
    {
        if (_deletedAt is null)
        {
            _deletedAt = DateTime.UtcNow;
            _updatedAt = DateTime.UtcNow;
        }
        return this;
    }

    public Customer Activate()
    {
        if (_deletedAt is not null)
        {
            _deletedAt = null;
            _updatedAt = DateTime.UtcNow;
        }
        return this;
    }

    public Customer UpdatePassword(string newPassword)
    {
        _password = newPassword;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Customer AddCpf(Cpf cpf)
    {
        _cpf = cpf;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new CustomerValidator(this, handler).Validate();
    }

    // 🧱 Propriedades públicas expostas (read-only)

    public string Name => _name;
    public Email Email => _email;
    public string Password => _password;
    public Cpf? Cpf => _cpf;

    public IReadOnlyCollection<CustomerAddress> Addresses =>
        (_addresses ?? new List<CustomerAddress>()).AsReadOnly();

    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;
    public DateTime? DeletedAt => _deletedAt;
    public bool IsActive => _deletedAt is null;

    public object Clone()
    {
        return With(
            Id,
            Name,
            Email,
            Password,
            Cpf,
            _addresses?.Select(a =>
                CustomerAddress.With(
                    a.Id,
                    a.Street,
                    a.Number,
                    a.City,
                    a.State,
                    a.ZipCode,
                    a.CreatedAt,
                    a.UpdatedAt
                )).ToList(),
            DeletedAt,
            CreatedAt,
            UpdatedAt
        );
    }
}


