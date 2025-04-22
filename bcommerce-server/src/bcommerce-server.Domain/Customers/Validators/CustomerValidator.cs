using System.Text.RegularExpressions;
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
        ValidatePassword();
        ValidateCpf();
        ValidateAddresses();
        ValidateActivationStatus();
    }

    private void ValidateName()
    {
        var name = _customer.Name;

        if (name is null)
        {
            AddError("'nome' não pode ser nulo.");
            return;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            AddError("'nome' não pode estar em branco.");
            return;
        }

        var length = name.Trim().Length;
        if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
        {
            AddError($"'nome' deve ter entre {NAME_MIN_LENGTH} e {NAME_MAX_LENGTH} caracteres.");
        }
    }

    private void ValidateEmail()
    {
        var email = _customer.Email;

        if (email is null)
        {
            AddError("'email' não pode ser nulo.");
            return;
        }

        if (string.IsNullOrWhiteSpace(email.Address))
        {
            AddError("'email' não pode estar em branco.");
            return;
        }

        var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        if (!emailRegex.IsMatch(email.Address))
        {
            AddError("'email' está em um formato inválido.");
        }
    }

    private void ValidatePassword()
    {
        var password = _customer.Password;

        if (password is null)
        {
            AddError("'senha' não pode ser nula.");
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            AddError("'senha' não pode estar em branco.");
            return;
        }

        if (password.Length < 6)
        {
            AddError("'senha' deve ter pelo menos 6 caracteres.");
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            AddError("'senha' deve conter ao menos uma letra maiúscula.");
        }

        if (!Regex.IsMatch(password, @"\d"))
        {
            AddError("'senha' deve conter ao menos um número.");
        }
    }

    private void ValidateCpf()
    {
        var cpf = _customer.Cpf;

        if (cpf is null)
            return; // CPF é opcional

        if (string.IsNullOrWhiteSpace(cpf.Number))
        {
            AddError("'cpf' não pode estar em branco.");
            return;
        }

        var digitsOnly = new string(cpf.Number.Where(char.IsDigit).ToArray());

        if (digitsOnly.Length != 11)
        {
            AddError("'cpf' deve conter exatamente 11 dígitos numéricos.");
            return;
        }

        if (!IsValidCpf(digitsOnly))
        {
            AddError("'cpf' é inválido.");
        }
    }

    private bool IsValidCpf(string cpf)
    {
        if (cpf.All(d => d == cpf[0])) return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf[..9];
        int soma = tempCpf.Select((t, i) => int.Parse(t.ToString()) * multiplicador1[i]).Sum();
        int resto = soma % 11 < 2 ? 0 : 11 - (soma % 11);

        if (resto != int.Parse(cpf[9].ToString()))
            return false;

        tempCpf += resto;
        soma = tempCpf.Select((t, i) => int.Parse(t.ToString()) * multiplicador2[i]).Sum();
        resto = soma % 11 < 2 ? 0 : 11 - (soma % 11);

        return resto == int.Parse(cpf[10].ToString());
    }
    private void ValidateAddresses()
    {
        var addresses = _customer.Addresses;

        if (addresses is null || !addresses.Any())
            return; // Endereços são opcionais

        int index = 0;
        foreach (var address in addresses)
        {
            if (address is null)
            {
                AddError($"'endereços[{index}]' não pode ser nulo.");
                index++;
                continue;
            }

            if (string.IsNullOrWhiteSpace(address.Street))
                AddError($"'endereços[{index}].rua' não pode estar em branco.");

            if (string.IsNullOrWhiteSpace(address.Number))
                AddError($"'endereços[{index}].número' não pode estar em branco.");

            if (string.IsNullOrWhiteSpace(address.City))
                AddError($"'endereços[{index}].cidade' não pode estar em branco.");

            if (string.IsNullOrWhiteSpace(address.State))
                AddError($"'endereços[{index}].estado' não pode estar em branco.");

            if (string.IsNullOrWhiteSpace(address.ZipCode))
                AddError($"'endereços[{index}].cep' não pode estar em branco.");

            index++;
        }
    }

    private void ValidateActivationStatus()
    {
        if (!_customer.IsActive && _customer.DeletedAt is null)
        {
            AddError("'deletedAt' deve estar definido quando o cliente estiver inativo.");
        }
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}


