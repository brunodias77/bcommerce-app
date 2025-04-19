using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Customers.Validators;

public class CustomerValidator : Validator
{
    private const int NAME_MIN_LENGTH = 3;
    private const int NAME_MAX_LENGTH = 255;

    private readonly Customer _customer;

    public CustomerValidator(Customer customer, IValidationHandler handler)
        : base(handler)
    {
        _customer = customer ?? throw new ArgumentNullException(nameof(customer));
    }

    public override void Validate()
    {
        ValidateName();
        ValidateEmail();
        ValidateCpf();
        ValidateAddresses(); // agora addresses são opcionais
    }

    private void ValidateName()
    {
        var name = _customer.Name;

        if (name is null)
        {
            AddError("'name' should not be null");
            return;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            AddError("'name' should not be empty");
            return;
        }

        var length = name.Trim().Length;
        if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
        {
            AddError($"'name' must be between {NAME_MIN_LENGTH} and {NAME_MAX_LENGTH} characters");
        }
    }

    private void ValidateEmail()
    {
        var email = _customer.Email;

        if (email is null)
        {
            AddError("'email' should not be null");
            return;
        }

        if (string.IsNullOrWhiteSpace(email.Address))
        {
            AddError("'email' should not be empty");
        }
    }

    private void ValidateCpf()
    {
        var cpf = _customer.Cpf;

        if (cpf is null)
        {
            AddError("'cpf' should not be null");
            return;
        }

        if (string.IsNullOrWhiteSpace(cpf.Number))
        {
            AddError("'cpf' should not be empty");
        }
        else if (cpf.Number.Length != 11)
        {
            AddError("'cpf' must contain 11 numeric digits");
        }
    }

    private void ValidateAddresses()
    {
        var addresses = _customer.Addresses;

        // Address é opcional: só valida se houver pelo menos um
        if (addresses is null || !addresses.Any())
            return;

        int index = 0;
        foreach (var address in addresses)
        {
            if (address is null)
            {
                AddError($"'addresses[{index}]' should not be null");
                index++;
                continue;
            }

            if (string.IsNullOrWhiteSpace(address.Street))
                AddError($"'addresses[{index}].street' should not be empty");

            if (string.IsNullOrWhiteSpace(address.Number))
                AddError($"'addresses[{index}].number' should not be empty");

            if (string.IsNullOrWhiteSpace(address.City))
                AddError($"'addresses[{index}].city' should not be empty");

            if (string.IsNullOrWhiteSpace(address.State))
                AddError($"'addresses[{index}].state' should not be empty");

            if (string.IsNullOrWhiteSpace(address.ZipCode))
                AddError($"'addresses[{index}].zipCode' should not be empty");

            index++;
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}
