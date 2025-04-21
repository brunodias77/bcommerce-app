using bcommerce_server.Domain.Addresses;
using bcommerce_server.Domain.Customers.Identifiers;
using bcommerce_server.Domain.Customers.Validators;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Customers;

public class Customer : AggregateRoot<CustomerID>
{
    private string _name;
    private Email _email;
    private string _password; // 🆕 NOVO
    private Cpf? _cpf;
    private List<Address>? _addresses;
    private bool _deleted;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    private Customer(
        CustomerID id,
        string name,
        Email email,
        string password,
        Cpf? cpf,
        List<Address>? addresses,
        bool deleted,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _name = name;
        _email = email;
        _password = password; // 🆕 NOVO
        _cpf = cpf;
        _addresses = addresses;
        _deleted = deleted;
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
            null,
            false,
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
        List<Address>? addresses,
        bool deleted,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new Customer(id, name, email,password, cpf, addresses, deleted, createdAt, updatedAt);
    }

    public Customer Update(string name, Email email, string password)
    {
        _name = name;
        _email = email;
        _password = password;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Customer AddAddress(Address address)
    {
        _addresses ??= new List<Address>(); // 🛡️ Protege contra null
        _addresses.Add(address);
        _updatedAt = DateTime.UtcNow;
        return this;
    }
    
    
    // 🆕 NOVO: método para atualizar a senha
    public Customer UpdatePassword(string newPassword)
    {
        _password = newPassword;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    // 🆕 NOVO: método para adicionar CPF separadamente
    public Customer AddCpf(Cpf cpf)
    {
        _cpf = cpf;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Customer Delete()
    {
        _deleted = true;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new CustomerValidator(this, handler).Validate();
    }

    // Propriedades públicas
    public string Name => _name;
    public Email Email => _email;
    public string Password => _password;
    public Cpf? Cpf => _cpf;
    public IReadOnlyCollection<Address> Addresses => 
        (_addresses != null ? _addresses : new List<Address>()).AsReadOnly();
    public bool IsDeleted => _deleted;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;

    public object Clone()
    {
        return With(
            Id,
            Name,
            Email,
            Password,
            Cpf,
            _addresses?.ToList(), // 🔄 safe null
            IsDeleted,
            CreatedAt,
            UpdatedAt
        );    
    }
}

