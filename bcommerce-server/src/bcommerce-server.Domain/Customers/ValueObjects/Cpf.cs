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
        Number = new string(number.Where(char.IsDigit).ToArray());
    }

    public static Cpf From(string raw) => new(raw);

    public override string ToString()
        => Convert.ToUInt64(Number).ToString(@"000\.000\.000\-00");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}
