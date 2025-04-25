using bcommerce_server.Domain.Customers.Identifiers;
using bcommerce_server.Domain.Customers.Validators;
using bcommerce_server.Domain.SeedWork;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Customers.Entities;

public class CustomerAddress: Entity<CustomerAddressID>
{
    private string _street;
    private string _number;
    private string _city;
    private string _state;
    private string _zipCode;
    private DateTime _createdAt;
    private DateTime _updatedAt;


    private CustomerAddress(
        CustomerAddressID id,
        string street,
        string number,
        string city,
        string state,
        string zipCode,
        DateTime createdAt,
        DateTime updatedAt
    ) : base(id)
    {
        _street = street;
        _number = number;
        _city = city;
        _state = state;
        _zipCode = zipCode;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public static CustomerAddress Create(
        string street,
        string number,
        string city,
        string state,
        string zipCode
    )
    {
        var now = DateTime.UtcNow;
        return new CustomerAddress(
            CustomerAddressID.Generate(),
            street,
            number,
            city,
            state,
            zipCode,
            now,
            now
        );
    }

    public static CustomerAddress With(
        CustomerAddressID id,
        string street,
        string number,
        string city,
        string state,
        string zipCode,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new CustomerAddress(id, street, number, city, state, zipCode, createdAt, updatedAt);
    }

    public CustomerAddress Update(
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
        new CustomerAddressValidator(this, handler).Validate();
    }

    // Getters públicos
    public string Street => _street;
    public string Number => _number;
    public string City => _city;
    public string State => _state;
    public string ZipCode => _zipCode;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;
}