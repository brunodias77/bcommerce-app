using bcommerce_server.Domain.Customers.ValueObjects;
using FluentAssertions;
using Xunit;

namespace bcommerce_server.UnitTest.Domain.Customers.ValueObjects;

public class EmailTest
{
    [Fact(DisplayName = "Deve normalizar email para lowercase")]
    public void CriarEmailComLowercase()
    {
        var email = Email.From("TeStE@Exemplo.COM");

        email.Address.Should().Be("teste@exemplo.com");
        email.ToString().Should().Be("teste@exemplo.com");
    }

    [Fact(DisplayName = "Dois emails com mesmo valor devem ser iguais")]
    public void EmailComMesmoValorSaoIguais()
    {
        var email1 = Email.From("teste@exemplo.com");
        var email2 = Email.From("TESTE@EXEMPLO.COM");

        email1.Should().Be(email2);
    }

    [Fact(DisplayName = "Emails diferentes não devem ser iguais")]
    public void EmailsDiferentesNaoSaoIguais()
    {
        var email1 = Email.From("teste1@exemplo.com");
        var email2 = Email.From("teste2@exemplo.com");

        email1.Should().NotBe(email2);
    }
}