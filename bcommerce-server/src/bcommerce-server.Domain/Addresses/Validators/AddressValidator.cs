using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Addresses.Validators;

public class AddressValidator : Validator
{
    private readonly Address _address;

    public AddressValidator(Address address, IValidationHandler handler)
        : base(handler)
    {
        _address = address ?? throw new ArgumentNullException(nameof(address));
    }

    public override void Validate()
    {
        ValidateStreet();
        ValidateNumber();
        ValidateCity();
        ValidateState();
        ValidateZipCode();
    }

    private void ValidateStreet()
    {
        if (string.IsNullOrWhiteSpace(_address.Street))
            AddError("A rua do endereço não pode estar em branco.");
    }

    private void ValidateNumber()
    {
        if (string.IsNullOrWhiteSpace(_address.Number))
            AddError("O número do endereço não pode estar em branco.");
    }

    private void ValidateCity()
    {
        if (string.IsNullOrWhiteSpace(_address.City))
            AddError("A cidade do endereço não pode estar em branco.");
    }

    private void ValidateState()
    {
        if (string.IsNullOrWhiteSpace(_address.State))
            AddError("O estado do endereço não pode estar em branco.");
    }

    private void ValidateZipCode()
    {
        if (string.IsNullOrWhiteSpace(_address.ZipCode))
            AddError("O CEP do endereço não pode estar em branco.");
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}


// public class AddressValidator : Validator
// {
//     private readonly Address _address;
//
//     public AddressValidator(Address address, IValidationHandler handler)
//         : base(handler)
//     {
//         _address = address ?? throw new ArgumentNullException(nameof(address));
//     }
//
//     public override void Validate()
//     {
//         ValidateStreet();
//         ValidateNumber();
//         ValidateCity();
//         ValidateState();
//         ValidateZipCode();
//     }
//
//     private void ValidateStreet()
//     {
//         if (string.IsNullOrWhiteSpace(_address.Street))
//         {
//             AddError("'street' should not be empty");
//         }
//     }
//
//     private void ValidateNumber()
//     {
//         if (string.IsNullOrWhiteSpace(_address.Number))
//         {
//             AddError("'number' should not be empty");
//         }
//     }
//
//     private void ValidateCity()
//     {
//         if (string.IsNullOrWhiteSpace(_address.City))
//         {
//             AddError("'city' should not be empty");
//         }
//     }
//
//     private void ValidateState()
//     {
//         if (string.IsNullOrWhiteSpace(_address.State))
//         {
//             AddError("'state' should not be empty");
//         }
//     }
//
//     private void ValidateZipCode()
//     {
//         if (string.IsNullOrWhiteSpace(_address.ZipCode))
//         {
//             AddError("'zipCode' should not be empty");
//         }
//     }
//
//     private void AddError(string message)
//     {
//         ValidationHandler.Append(new Error(message));
//     }
// }