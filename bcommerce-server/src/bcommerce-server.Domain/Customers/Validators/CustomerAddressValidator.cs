using bcommerce_server.Domain.Customers.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Customers.Validators;

public class CustomerAddressValidator : Validator
{
    private readonly CustomerAddress _address;

    public CustomerAddressValidator(CustomerAddress address, IValidationHandler handler)
        : base(handler)
    {
        _address = address ?? throw new ArgumentNullException(nameof(address));
    }

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(_address.Street))
            AddError("'rua' não pode estar em branco.");

        if (string.IsNullOrWhiteSpace(_address.Number))
            AddError("'número' não pode estar em branco.");

        if (string.IsNullOrWhiteSpace(_address.City))
            AddError("'cidade' não pode estar em branco.");

        if (string.IsNullOrWhiteSpace(_address.State))
            AddError("'estado' não pode estar em branco.");

        if (string.IsNullOrWhiteSpace(_address.ZipCode))
            AddError("'cep' não pode estar em branco.");
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}