using bcommerce_server.Domain.Addresses;
using bcommerce_server.Domain.Customers.Identifiers;
using bcommerce_server.Domain.Validations.Handlers;
using FluentAssertions;
using Xunit;

namespace bcommerce_server.UnitTest.Domain.Addresses;

public class AddressTest
{
    private readonly CustomerID _customerId = CustomerID.Generate();

    [Fact(DisplayName = "Deve instanciar um endereço com sucesso")]
    public void CriarEnderecoValido()
    {
        var address = Address.NewAddress(_customerId, "Rua A", "123", "Cidade Z", "SP", "01000-000");

        address.Should().NotBeNull();
        address.Street.Should().Be("Rua A");
        address.Number.Should().Be("123");
        address.City.Should().Be("Cidade Z");
        address.State.Should().Be("SP");
        address.ZipCode.Should().Be("01000-000");
        address.CustomerId.Should().Be(_customerId);
    }

    [Fact(DisplayName = "Deve atualizar endereço corretamente")]
    public void AtualizarEndereco()
    {
        var address = Address.NewAddress(_customerId, "Rua A", "123", "Cidade Z", "SP", "01000-000");

        address.Update("Rua Nova", "999", "Cidade Nova", "RJ", "12345-678");

        address.Street.Should().Be("Rua Nova");
        address.Number.Should().Be("999");
        address.City.Should().Be("Cidade Nova");
        address.State.Should().Be("RJ");
        address.ZipCode.Should().Be("12345-678");
    }

    [Fact(DisplayName = "Clone deve retornar uma cópia com os mesmos valores")]
    public void ClonarEndereco()
    {
        var original = Address.NewAddress(_customerId, "Rua A", "123", "Cidade Z", "SP", "01000-000");
        var clone = (Address)original.Clone();

        clone.Should().NotBeSameAs(original);
        clone.Should().BeEquivalentTo(original);
    }

    [Fact(DisplayName = "Deve validar endereço com sucesso")]
    public void ValidarEnderecoCorreto()
    {
        var address = Address.NewAddress(_customerId, "Rua A", "123", "Cidade Z", "SP", "01000-000");
        var notification = Notification.Create();

        address.Validate(notification);

        notification.HasError().Should().BeFalse();
    }

    [Fact(DisplayName = "Deve retornar erro se campos obrigatórios estiverem vazios")]
    public void ValidarEnderecoInvalido()
    {
        var address = Address.NewAddress(_customerId, "", "", "", "", "");
        var notification = Notification.Create();

        address.Validate(notification);

        notification.HasError().Should().BeTrue();
        notification.GetErrors().Should().HaveCountGreaterThan(0);
    }
}