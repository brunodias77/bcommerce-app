using Bogus;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.Customers.Identifiers;
using bcommerce_server.Domain.Addresses;
using bcommerce_server.UnitTest.Common;
using Xunit;

namespace bcommerce_server.UnitTests.Domain.Customers;

public class CustomerTestFixture : BaseFixture
{
    public CustomerTestFixture() : base()
    {
    }

    public string GetValidCustomerName()
    {
        var name = "";
        while (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            name = Faker.Name.FullName();

        return name.Length > 255 ? name[..255] : name;
    }

    public Email GetValidEmail()
    {
        var rawEmail = Faker.Internet.Email();
        return Email.From(rawEmail);
    }

    public string GetValidPassword()
    {
        string password = "";
        while (!IsPasswordValid(password))
        {
            password = $"{Faker.Internet.Password(6, false)}A1";
        }
        return password;
    }

    public Cpf GetValidCpf()
    {
        return Cpf.From(GenerateValidCpf());
    }

    private string GenerateValidCpf()
    {
        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var random = new Random();
        var seed = Enumerable.Range(0, 9).Select(_ => random.Next(0, 10)).ToArray();

        int soma = seed.Select((num, i) => num * multiplicador1[i]).Sum();
        int resto = soma % 11;
        var primeiroDigito = resto < 2 ? 0 : 11 - resto;

        var seedComPrimeiro = seed.Concat(new[] { primeiroDigito }).ToArray();
        soma = seedComPrimeiro.Select((num, i) => num * multiplicador2[i]).Sum();
        resto = soma % 11;
        var segundoDigito = resto < 2 ? 0 : 11 - resto;

        return string.Concat(seed) + primeiroDigito + segundoDigito;
    }

    public CustomerID GetValidCustomerId()
        => CustomerID.Generate();

    public Address GetValidAddress(CustomerID customerId)
    {
        string street = "", number = "", city = "", state = "", zipCode = "";

        while (string.IsNullOrWhiteSpace(street))
            street = Faker.Address.StreetName();

        while (string.IsNullOrWhiteSpace(number))
            number = Faker.Address.BuildingNumber();

        while (string.IsNullOrWhiteSpace(city))
            city = Faker.Address.City();

        while (string.IsNullOrWhiteSpace(state))
            state = Faker.Address.StateAbbr();

        while (string.IsNullOrWhiteSpace(zipCode))
            zipCode = Faker.Address.ZipCode("#####-###");

        return Address.NewAddress(
            customerId,
            street,
            number,
            city,
            state,
            zipCode
        );
    }

    public Customer GetValidCustomer()
    {
        return Customer.NewCustomer(
            GetValidCustomerName(),
            GetValidEmail(),
            GetValidPassword()
        );
    }

    public Customer GetValidCustomerWithCpfAndAddress()
    {
        var customer = GetValidCustomer();
        customer.AddCpf(GetValidCpf());
        customer.AddAddress(GetValidAddress(customer.Id));
        return customer;
    }

    private bool IsPasswordValid(string password)
    {
        return password.Length >= 6 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsDigit);
    }
}

[CollectionDefinition(nameof(CustomerTestFixture))]
public class CustomerTestFixtureCollection : ICollectionFixture<CustomerTestFixture>
{
}
