using bcommerce_server.Domain.Addresses.Identifiers;
using bcommerce_server.Domain.Customers.Identifiers;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Addresses;

public class Address : Entity<AddressID>
{
    private CustomerID _customerId;
    private string _street;
    private string _number;
    private string _city;
    private string _state;
    private string _zipCode;
    private DateTime _createdAt;

    private Address(
        AddressID id,
        CustomerID customerId,
        string street,
        string number,
        string city,
        string state,
        string zipCode,
        DateTime createdAt
    ) : base(id)
    {
        _customerId = customerId;
        _street = street;
        _number = number;
        _city = city;
        _state = state;
        _zipCode = zipCode;
        _createdAt = createdAt;
    }

    public static Address NewAddress(
        CustomerID customerId,
        string street,
        string number,
        string city,
        string state,
        string zipCode
    )
    {
        var now = DateTime.UtcNow;
        return new Address(
            AddressID.Generate(),
            customerId,
            street,
            number,
            city,
            state,
            zipCode,
            now
        );
    }

    public static Address With(
        AddressID id,
        CustomerID customerId,
        string street,
        string number,
        string city,
        string state,
        string zipCode,
        DateTime createdAt
    )
    {
        return new Address(id, customerId, street, number, city, state, zipCode, createdAt);
    }

    public Address Update(
        string street,
        string number,
        string city,
        string state,
        string zipCode
    )
    {
        _street = street;
        _number = number;
        _city = city;
        _state = state;
        _zipCode = zipCode;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        // Você pode criar um AddressValidator se quiser, como no Customer
        // new AddressValidator(this, handler).Validate();
    }

    // Propriedades públicas
    public CustomerID CustomerId => _customerId;
    public string Street => _street;
    public string Number => _number;
    public string City => _city;
    public string State => _state;
    public string ZipCode => _zipCode;
    public DateTime CreatedAt => _createdAt;

    public object Clone()
    {
        return With(Id, _customerId, _street, _number, _city, _state, _zipCode, _createdAt);
    }
}