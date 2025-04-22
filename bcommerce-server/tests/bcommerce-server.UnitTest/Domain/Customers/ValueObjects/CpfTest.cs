using bcommerce_server.Domain.Customers.ValueObjects;
using FluentAssertions;
using Xunit;

namespace bcommerce_server.UnitTest.Domain.Customers.ValueObjects;

public class CpfTest
{
    [Fact(DisplayName = "Deve criar um CPF e formatar corretamente")]
    public void CriarCpfComFormatacaoCorreta()
    {
        var cpf = Cpf.From("123.456.789-09");

        cpf.Number.Should().Be("12345678909");
        cpf.ToString().Should().Be("123.456.789-09");
    }

    [Fact(DisplayName = "Dois CPFs com mesmo número devem ser iguais")]
    public void CpfComMesmoNumeroSaoIguais()
    {
        var cpf1 = Cpf.From("111.222.333-44");
        var cpf2 = Cpf.From("11122233344");

        cpf1.Should().Be(cpf2);
        cpf1.Equals(cpf2).Should().BeTrue();
    }

    [Fact(DisplayName = "CPFs diferentes devem ser diferentes")]
    public void CpfDiferenteNaoSaoIguais()
    {
        var cpf1 = Cpf.From("111.222.333-44");
        var cpf2 = Cpf.From("000.000.000-00");

        cpf1.Should().NotBe(cpf2);
    }
}