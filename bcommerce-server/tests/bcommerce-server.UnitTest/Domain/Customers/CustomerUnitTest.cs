using bcommerce_server.Domain.Addresses;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.UnitTests.Domain.Customers;
using FluentAssertions;
using Xunit;

namespace bcommerce_server.UnitTest.Domain.Customers;

[Collection(nameof(CustomerTestFixture))]
public class CustomerUnitTest
{
    private readonly CustomerTestFixture _fixture;

    public CustomerUnitTest(CustomerTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(INSTANCIAR_COM_SUCESSO))]
    [Trait("Domain", "Customer - Aggregates")]
    public void INSTANCIAR_COM_SUCESSO()
    {
        var name = _fixture.GetValidCustomerName();
        var email = _fixture.GetValidEmail();
        var password = _fixture.GetValidPassword();
        var before = DateTime.UtcNow;

        var customer = Customer.NewCustomer(name, email, password);
        var after = DateTime.UtcNow;

        customer.Should().NotBeNull();
        customer.Name.Should().Be(name);
        customer.Email.Should().Be(email);
        customer.Password.Should().Be(password);
        customer.Cpf.Should().BeNull();
        customer.Addresses.Should().BeEmpty();
        customer.IsActive.Should().BeTrue();
        customer.DeletedAt.Should().BeNull();
        customer.CreatedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
        customer.UpdatedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact(DisplayName = nameof(ADICIONAR_CPF))]
    [Trait("Domain", "Customer - Aggregates")]
    public void ADICIONAR_CPF()
    {
        var customer = _fixture.GetValidCustomer();
        var cpf = _fixture.GetValidCpf();

        customer.AddCpf(cpf);

        customer.Cpf.Should().NotBeNull();
        customer.Cpf!.Number.Should().Be(cpf.Number);
    }

    [Fact(DisplayName = nameof(ADICIONAR_ENDERECO))]
    [Trait("Domain", "Customer - Aggregates")]
    public void ADICIONAR_ENDERECO()
    {
        var customer = _fixture.GetValidCustomer();
        var address = _fixture.GetValidAddress(customer.Id);

        customer.AddAddress(address);

        customer.Addresses.Should().ContainSingle()
            .And.ContainEquivalentOf(address);
    }

    [Fact(DisplayName = nameof(ATUALIZAR_DADOS))]
    [Trait("Domain", "Customer - Aggregates")]
    public void ATUALIZAR_DADOS()
    {
        var customer = _fixture.GetValidCustomer();
        var newName = _fixture.GetValidCustomerName();
        var newEmail = _fixture.GetValidEmail();
        var newPassword = _fixture.GetValidPassword();
        var newCpf = _fixture.GetValidCpf();

        customer.Update(newName, newEmail, newPassword, newCpf);

        customer.Name.Should().Be(newName);
        customer.Email.Should().Be(newEmail);
        customer.Password.Should().Be(newPassword);
        customer.Cpf.Should().BeEquivalentTo(newCpf);
    }

    [Fact(DisplayName = nameof(ATUALIZAR_SENHA))]
    [Trait("Domain", "Customer - Aggregates")]
    public void ATUALIZAR_SENHA()
    {
        var customer = _fixture.GetValidCustomer();
        var newPassword = _fixture.GetValidPassword();

        customer.UpdatePassword(newPassword);

        customer.Password.Should().Be(newPassword);
    }

    [Fact(DisplayName = nameof(ATIVAR_E_DESATIVAR))]
    [Trait("Domain", "Customer - Aggregates")]
    public void ATIVAR_E_DESATIVAR()
    {
        var customer = _fixture.GetValidCustomer();

        customer.Deactivate();
        customer.IsActive.Should().BeFalse();
        customer.DeletedAt.Should().NotBeNull();

        customer.Activate();
        customer.IsActive.Should().BeTrue();
        customer.DeletedAt.Should().BeNull();
    }

    [Fact(DisplayName = nameof(VALIDAR_CLIENTE_VALIDO))]
    [Trait("Domain", "Customer - Validation")]
    public void VALIDAR_CLIENTE_VALIDO()
    {
        var customer = _fixture.GetValidCustomerWithCpfAndAddress();
        var notification = Notification.Create();

        customer.Validate(notification);

        notification.HasError().Should().BeFalse();
    }

    [Theory(DisplayName = nameof(INVALIDAR_SENHAS_FRACAS))]
    [Trait("Domain", "Customer - Validation")]
    [InlineData("abc")]
    [InlineData("semmaiuscula1")]
    [InlineData("SEMNUMERO")]
    public void INVALIDAR_SENHAS_FRACAS(string weakPassword)
    {
        var name = _fixture.GetValidCustomerName();
        var email = _fixture.GetValidEmail();

        var customer = Customer.NewCustomer(name, email, weakPassword);
        var notification = Notification.Create();

        customer.Validate(notification);

        notification.HasError().Should().BeTrue();
        notification.GetErrors().Should().Contain(e => e.Message.ToLower().Contains("senha"));
    }

    [Theory(DisplayName = nameof(INVALIDAR_EMAILS_INVALIDOS))]
    [Trait("Domain", "Customer - Validation")]
    [InlineData("emailinvalido")]
    [InlineData("sem-arroba.com")]
    [InlineData("@semusuario.com")]
    public void INVALIDAR_EMAILS_INVALIDOS(string rawEmail)
    {
        var name = _fixture.GetValidCustomerName();
        var password = _fixture.GetValidPassword();
        var email = Email.From(rawEmail);

        var customer = Customer.NewCustomer(name, email, password);
        var notification = Notification.Create();

        customer.Validate(notification);

        notification.HasError().Should().BeTrue();
        notification.GetErrors().Should().Contain(e => e.Message.ToLower().Contains("email"));
    }

    [Fact(DisplayName = nameof(INVALIDAR_COM_ENDERECO_INCORRETO))]
    [Trait("Domain", "Customer - Validation")]
    public void INVALIDAR_COM_ENDERECO_INCORRETO()
    {
        var customer = _fixture.GetValidCustomer();
        var invalidAddress = Address.NewAddress(customer.Id, "", "", "", "", "");
        customer.AddAddress(invalidAddress);

        var notification = Notification.Create();
        customer.Validate(notification);

        notification.HasError().Should().BeTrue();
        notification.GetErrors().Should().Contain(e => e.Message.ToLower().Contains("endereço"));
    }
}
