using bcommerce_server.Domain.Addresses.Identifiers;
using bcommerce_server.Domain.Addresses.Validators;
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
    private DateTime _updatedAt;

    private Address(
        AddressID id,
        CustomerID customerId,
        string street,
        string number,
        string city,
        string state,
        string zipCode,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _customerId = customerId;
        _street = street;
        _number = number;
        _city = city;
        _state = state;
        _zipCode = zipCode;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
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
            now,
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
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new Address(id, customerId, street, number, city, state, zipCode, createdAt, updatedAt);
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
        _updatedAt = DateTime.UtcNow;
        return this;
    }

    public override void Validate(IValidationHandler handler)
    {
        new AddressValidator(this, handler).Validate();
    }

    public CustomerID CustomerId => _customerId;
    public string Street => _street;
    public string Number => _number;
    public string City => _city;
    public string State => _state;
    public string ZipCode => _zipCode;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;

    public object Clone()
    {
        return With(Id, _customerId, _street, _number, _city, _state, _zipCode, _createdAt, _updatedAt);
    }
}