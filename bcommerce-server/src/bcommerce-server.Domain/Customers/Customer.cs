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
    private Cpf _cpf;
    private readonly List<Address> _addresses = new();
    private bool _deleted;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    private Customer(
        CustomerID id,
        string name,
        Email email,
        Cpf cpf,
        List<Address> addresses,
        bool deleted,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _name = name;
        _email = email;
        _cpf = cpf;
        _addresses = addresses;
        _deleted = deleted;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public static Customer NewCustomer(string name, Email email, Cpf cpf)
    {
        var now = DateTime.UtcNow;
        return new Customer(
            CustomerID.Generate(),
            name,
            email,
            cpf,
            new List<Address>(),
            false,
            now,
            now
        );
    }

    public static Customer With(
        CustomerID id,
        string name,
        Email email,
        Cpf cpf,
        List<Address> addresses,
        bool deleted,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new Customer(id, name, email, cpf, addresses, deleted, createdAt, updatedAt);
    }

    public Customer Update(string name, Email email)
    {
        _name = name;
        _email = email;
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public Customer AddAddress(Address address)
    {
        _addresses.Add(address);
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
    public Cpf Cpf => _cpf;
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
    public bool IsDeleted => _deleted;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;

    public object Clone()
    {
        return With(Id, Name, Email, Cpf, _addresses.ToList(), IsDeleted, CreatedAt, UpdatedAt);
    }
}

// public class Customer : AggregateRoot<CustomerID>
// {
//     private string _name;
//     private Email _email;
//     private Cpf _cpf;
//     private Address _address;
//     private bool _deleted;
//     private DateTime _createdAt;
//     private DateTime _updatedAt;
//
//     private Customer(
//         CustomerID id,
//         string name,
//         Email email,
//         Cpf cpf,
//         Address address,
//         bool deleted,
//         DateTime createdAt,
//         DateTime updatedAt
//     ) : base(id)
//     {
//         _name = name;
//         _email = email;
//         _cpf = cpf;
//         _address = address;
//         _deleted = deleted;
//         _createdAt = createdAt;
//         _updatedAt = updatedAt;
//     }
//
//     public static Customer NewCustomer(string name, Email email, Cpf cpf, Address address)
//     {
//         var now = DateTime.UtcNow;
//         return new Customer(
//             CustomerID.Generate(),
//             name,
//             email,
//             cpf,
//             address,
//             false,
//             now,
//             now
//         );
//     }
//
//     public static Customer With(Customer customer)
//     {
//         return new Customer(
//             customer.Id,
//             customer._name,
//             customer._email,
//             customer._cpf,
//             customer._address,
//             customer._deleted,
//             customer._createdAt,
//             customer._updatedAt
//         );
//     }
//
//     public static Customer With(CustomerID id, string name, Email email, Cpf cpf, Address address, bool deleted, DateTime createdAt, DateTime updatedAt)
//     {
//         return new Customer(id, name, email, cpf, address, deleted, createdAt, updatedAt);
//     }
//
//     public Customer Update(string name, Email email, Address address)
//     {
//         _name = name;
//         _email = email;
//         _address = address;
//         _updatedAt = DateTime.UtcNow;
//         return this;
//     }
//
//     public Customer Delete()
//     {
//         _deleted = true;
//         _updatedAt = DateTime.UtcNow;
//         return this;
//     }
//
//     public override void Validate(IValidationHandler handler)
//     {
//         new CustomerValidator(this, handler).Validate();
//     }
//
//     // Propriedades públicas
//     public string Name => _name;
//     public Email Email => _email;
//     public Cpf Cpf => _cpf;
//     public Address Address => _address;
//     public bool IsDeleted => _deleted;
//     public DateTime CreatedAt => _createdAt;
//     public DateTime UpdatedAt => _updatedAt;
//     
//     // Clone (defensive copy)
//     public object Clone()
//     {
//         return With(this);
//     }
//}