using bcommerce_server.Application.Customers.Create;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.Security;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.Infra.Repositories;
using bcommerce_server.UnitTest.Common;
using bcommerce_server.Domain.Validations;
using Bogus;
using Moq;

namespace bcommerce_server.UnitTest.Application.Common;

// Fixture base com utilitários e mocks para testes de casos de uso relacionados a cliente
public abstract class CustomerUseCaseBaseFixture : BaseFixture
{
    protected readonly Faker Faker; // Gerador de dados falsos

    public Mock<ICustomerRepository> CustomerRepositoryMock { get; }    // Mock do repositório de clientes
    public Mock<IUnitOfWork> UnitOfWorkMock { get; }                    // Mock do unit of work
    public Mock<IPasswordEncripter> PasswordEncripterMock { get; }      // Mock de encriptador de senha

    protected CustomerUseCaseBaseFixture()
    {
        Faker = new Faker("pt_BR");                       // Gera dados brasileiros realistas
        CustomerRepositoryMock = new Mock<ICustomerRepository>();
        UnitOfWorkMock = new Mock<IUnitOfWork>();
        PasswordEncripterMock = new Mock<IPasswordEncripter>();
    }

    // Cria a instância real do caso de uso com dependências mockadas
    public CreateCustomerUseCase CreateUseCase()
    {
        return new CreateCustomerUseCase(
            CustomerRepositoryMock.Object,
            UnitOfWorkMock.Object,
            PasswordEncripterMock.Object
        );
    }

    // Retorna um nome aleatório válido
    public string GetValidName() => Faker.Name.FullName();

    // Retorna um e-mail válido em letras minúsculas
    public string GetValidEmail() => Faker.Internet.Email().ToLowerInvariant();

    // Gera uma senha com no mínimo 6 caracteres, uma letra maiúscula e um número
    public string GetValidPassword() =>
        $"{Faker.Internet.Password(6, false, "", "A")}{Faker.Random.Number(0, 9)}";

    // Gera senha inválida para testes
    public string GetInvalidPassword() => "abc";

    // Cria um input completo válido para o use case
    public CreateCustomerInput GetValidInput()
    {
        return new CreateCustomerInput(
            GetValidName(),
            GetValidEmail(),
            GetValidPassword()
        );
    }

    // Cria uma entidade Customer válida, útil para simular retorno do repositório
    public Customer GetValidCustomer(string? name = null, string? email = null, string? encryptedPassword = null)
    {
        return Customer.NewCustomer(
            name ?? GetValidName(),
            Email.From(email ?? GetValidEmail()),
            encryptedPassword ?? PasswordEncripterMock.Object.Encrypt(GetValidPassword())
        );
    }

    // Cria uma notification com erro para facilitar simulação de falhas
    public Notification GetNotificationWith(string message)
    {
        return Notification.Create(new Error(message));
    }
}