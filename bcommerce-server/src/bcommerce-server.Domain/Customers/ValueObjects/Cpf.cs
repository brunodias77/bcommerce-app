using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Customers.ValueObjects;

/// <summary>
/// Representa um CPF válido (Cadastro de Pessoa Física).
/// </summary>
public sealed class Cpf : ValueObject
{
    public string Number { get; }

    private Cpf(string number)
    {
        var onlyDigits = new string(number.Where(char.IsDigit).ToArray());

        if (onlyDigits.Length != 11 || !IsValid(onlyDigits))
            throw new ArgumentException("CPF inválido.");

        Number = onlyDigits;
    }

    public static Cpf From(string number) => new(number);

    public override string ToString()
        => Convert.ToUInt64(Number).ToString(@"000\.000\.000\-00");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }

    private static bool IsValid(string cpf)
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
}