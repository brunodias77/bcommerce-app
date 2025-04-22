// Usando namespaces necessários para testes, domínio e mocks
using System;
using System.Threading;
using System.Threading.Tasks;
using bcommerce_server.Application.Customers.Create;
using bcommerce_server.Domain.Customers;
using bcommerce_server.UnitTest.Application.Common;
using FluentAssertions;
using Moq;
using Xunit;

namespace bcommerce_server.UnitTest.Application.Customers.Create;

// Classe de testes para CreateCustomerUseCase, herda da fixture base
public class CreateCustomerUseCaseTests : CustomerUseCaseBaseFixture
{
    // Método de utilidade para configurar o comportamento padrão do UnitOfWork
    private void SetupUnitOfWorkMock()
    {
        UnitOfWorkMock.Setup(x => x.Begin()).Returns(Task.CompletedTask);         // Simula início de transação
        UnitOfWorkMock.Setup(x => x.Commit()).Returns(Task.CompletedTask);        // Simula commit de sucesso
        UnitOfWorkMock.Setup(x => x.Rollback()).Returns(Task.CompletedTask);      // Simula rollback
        UnitOfWorkMock.Setup(x => x.DisposeAsync()).Returns(ValueTask.CompletedTask); // Simula descarte
    }

    [Fact(DisplayName = "Deve criar um cliente com sucesso")]
    public async Task DeveCriarClienteComSucesso()
    {
        // Arrange
        var useCase = CreateUseCase(); // Cria instância do use case com mocks
        var input = GetValidInput();   // Gera dados de entrada válidos
        var encryptedPassword = $"encrypted_{input.Password}"; // Valor fictício criptografado

        PasswordEncripterMock
            .Setup(x => x.Encrypt(input.Password)) // Mocka criptografia
            .Returns(encryptedPassword);

        CustomerRepositoryMock
            .Setup(x => x.GetByEmail(input.Email, It.IsAny<CancellationToken>())) // Email não existente
            .ReturnsAsync((Customer?)null);

        SetupUnitOfWorkMock(); // Configura mock do UnitOfWork

        // Act
        var result = await useCase.Execute(input); // Executa o caso de uso

        // Assert
        result.IsSuccess.Should().BeTrue();           // Deve ser sucesso
        result.Success.Should().NotBeNull();          // Deve conter retorno

        var output = result.Success!;
        output.Name.Should().Be(input.Name);          // Nome deve bater
        output.Email.Should().Be(input.Email);        // Email deve bater
        output.Id.Should().NotBeEmpty();              // Id deve estar presente
        output.CreatedAt.Should().NotBe(default);     // Data deve estar setada
        output.IsActive.Should().BeTrue();            // Cliente deve estar ativo

        // Verificações dos efeitos colaterais
        CustomerRepositoryMock.Verify(x => x.Insert(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
        UnitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        UnitOfWorkMock.Verify(x => x.DisposeAsync(), Times.Once);
    }

    [Fact(DisplayName = "Não deve criar cliente com email duplicado")]
    public async Task NaoDeveCriarClienteComEmailDuplicado()
    {
        // Arrange
        var useCase = CreateUseCase();
        var input = GetValidInput();
        var existingCustomer = GetValidCustomer(email: input.Email); // Cliente já existente

        CustomerRepositoryMock
            .Setup(x => x.GetByEmail(input.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingCustomer); // Simula email duplicado

        SetupUnitOfWorkMock();

        // Act
        var result = await useCase.Execute(input);

        // Assert
        result.IsSuccess.Should().BeFalse();           // Deve falhar
        result.Error.Should().NotBeNull();             // Deve ter erros
        result.Error.GetErrors().Should().ContainSingle()
            .Which.Message.Should().Be("Já existe um cliente com este e-mail."); // Mensagem esperada
    }

    [Fact(DisplayName = "Não deve criar cliente com dados inválidos")]
    public async Task NaoDeveCriarClienteComDadosInvalidos()
    {
        // Arrange
        var useCase = CreateUseCase();
        var input = new CreateCustomerInput("", "email-invalido", "123"); // Dados inválidos

        CustomerRepositoryMock
            .Setup(x => x.GetByEmail(input.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        PasswordEncripterMock
            .Setup(x => x.Encrypt(input.Password))
            .Returns($"enc_{input.Password}");

        SetupUnitOfWorkMock();

        // Act
        var result = await useCase.Execute(input);

        // Assert
        result.IsSuccess.Should().BeFalse();               // Deve falhar
        var errors = result.Error.GetErrors();             // Captura erros de validação

        errors.Should().HaveCount(3);                      // Deve ter 3 erros
        errors.Should().Contain(e => e.Message.Contains("'nome' não pode estar em branco."));
        errors.Should().Contain(e => e.Message.Contains("'email' está em um formato inválido."));
        errors.Should().Contain(e => e.Message.Contains("'senha' deve conter ao menos uma letra maiúscula."));
    }

    [Fact(DisplayName = "Deve fazer rollback se ocorrer erro ao salvar cliente")]
    public async Task DeveFazerRollbackSeErroAoSalvarCliente()
    {
        // Arrange
        var useCase = CreateUseCase();
        var input = GetValidInput();
        var encryptedPassword = $"enc_{input.Password}";

        PasswordEncripterMock
            .Setup(x => x.Encrypt(input.Password))
            .Returns(encryptedPassword);

        CustomerRepositoryMock
            .Setup(x => x.GetByEmail(input.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        CustomerRepositoryMock
            .Setup(x => x.Insert(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro de banco")); // Simula erro no insert

        SetupUnitOfWorkMock();

        // Act
        var result = await useCase.Execute(input);

        // Assert
        result.IsSuccess.Should().BeFalse();                         // Deve falhar
        result.Error.GetErrors().Should().ContainSingle()
            .Which.Message.Should().Be("Erro de banco");             // Mensagem da exceção capturada
    }

    [Fact(DisplayName = "Não deve criar cliente com senha fraca")]
    public async Task NaoDeveCriarClienteComSenhaFraca()
    {
        // Arrange
        var useCase = CreateUseCase();
        var input = new CreateCustomerInput(GetValidName(), GetValidEmail(), "abcdef"); // Senha sem número

        CustomerRepositoryMock
            .Setup(x => x.GetByEmail(input.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        PasswordEncripterMock
            .Setup(x => x.Encrypt(input.Password))
            .Returns($"enc_{input.Password}");

        SetupUnitOfWorkMock();

        // Act
        var result = await useCase.Execute(input);

        // Assert
        result.IsSuccess.Should().BeFalse();
        var errors = result.Error.GetErrors();
        errors.Should().Contain(e => e.Message.Contains("'senha' deve conter ao menos uma letra maiúscula."));
        errors.Should().Contain(e => e.Message.Contains("'senha' deve conter ao menos um número."));
    }
}




// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using bcommerce_server.Application.Customers.Create;
// using bcommerce_server.Domain.Customers;
// using bcommerce_server.UnitTest.Application.Common;
// using FluentAssertions;
// using Moq;
// using Xunit;
//
// namespace bcommerce_server.UnitTest.Application.Customers.Create;
//
// public class CreateCustomerUseCaseTests : CustomerUseCaseBaseFixture
// {
//     private void SetupUnitOfWorkMock()
//     {
//         UnitOfWorkMock.Setup(x => x.Begin()).Returns(Task.CompletedTask);
//         UnitOfWorkMock.Setup(x => x.Commit()).Returns(Task.CompletedTask);
//         UnitOfWorkMock.Setup(x => x.Rollback()).Returns(Task.CompletedTask);
//         UnitOfWorkMock.Setup(x => x.DisposeAsync()).Returns(ValueTask.CompletedTask);
//     }
//
//     [Fact(DisplayName = "Deve criar um cliente com sucesso")]
//     public async Task DeveCriarClienteComSucesso()
//     {
//         // Arrange
//         var useCase = CreateUseCase();
//         var input = GetValidInput();
//         var encryptedPassword = $"encrypted_{input.Password}";
//
//         PasswordEncripterMock
//             .Setup(x => x.Encrypt(input.Password))
//             .Returns(encryptedPassword);
//
//         CustomerRepositoryMock
//             .Setup(x => x.GetByEmail(input.Email, It.IsAny<CancellationToken>()))
//             .ReturnsAsync((Customer?)null);
//
//         SetupUnitOfWorkMock();
//
//         // Act
//         var result = await useCase.Execute(input);
//
//         // Assert
//         result.IsSuccess.Should().BeTrue();
//         result.Success.Should().NotBeNull();
//
//         var output = result.Success!;
//         output.Name.Should().Be(input.Name);
//         output.Email.Should().Be(input.Email);
//         output.Id.Should().NotBeEmpty();
//         output.CreatedAt.Should().NotBe(default);
//         output.IsActive.Should().BeTrue();
//
//         CustomerRepositoryMock.Verify(x => x.Insert(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
//         UnitOfWorkMock.Verify(x => x.Commit(), Times.Once);
//         UnitOfWorkMock.Verify(x => x.DisposeAsync(), Times.Once);
//     }
//
//     [Fact(DisplayName = "Não deve criar cliente com email duplicado")]
//     public async Task NaoDeveCriarClienteComEmailDuplicado()
//     {
//         // Arrange
//         var useCase = CreateUseCase();
//         var input = GetValidInput();
//         var existingCustomer = GetValidCustomer(email: input.Email);
//
//         CustomerRepositoryMock
//             .Setup(x => x.GetByEmail(input.Email, It.IsAny<CancellationToken>()))
//             .ReturnsAsync(existingCustomer);
//
//         SetupUnitOfWorkMock();
//
//         // Act
//         var result = await useCase.Execute(input);
//
//         // Assert
//         result.IsSuccess.Should().BeFalse();
//         result.Error.Should().NotBeNull();
//         result.Error.GetErrors().Should().ContainSingle()
//             .Which.Message.Should().Be("Já existe um cliente com este e-mail.");
//     }
//
//     [Fact(DisplayName = "Não deve criar cliente com dados inválidos")]
//     public async Task NaoDeveCriarClienteComDadosInvalidos()
//     {
//         // Arrange
//         var useCase = CreateUseCase();
//         var input = new CreateCustomerInput("", "email-invalido", "123");
//
//         CustomerRepositoryMock
//             .Setup(x => x.GetByEmail(input.Email, It.IsAny<CancellationToken>()))
//             .ReturnsAsync((Customer?)null);
//
//         PasswordEncripterMock
//             .Setup(x => x.Encrypt(input.Password))
//             .Returns($"enc_{input.Password}");
//
//         SetupUnitOfWorkMock();
//
//         // Act
//         var result = await useCase.Execute(input);
//
//         // Assert
//         result.IsSuccess.Should().BeFalse();
//         var errors = result.Error.GetErrors();
//         errors.Should().HaveCount(3);
//         errors.Should().Contain(e => e.Message.Contains("'nome' não pode estar em branco."));
//         errors.Should().Contain(e => e.Message.Contains("'email' está em um formato inválido."));
//         errors.Should().Contain(e => e.Message.Contains("'senha' deve conter ao menos uma letra maiúscula."));
//
//     }
//
//     [Fact(DisplayName = "Deve fazer rollback se ocorrer erro ao salvar cliente")]
//     public async Task DeveFazerRollbackSeErroAoSalvarCliente()
//     {
//         // Arrange
//         var useCase = CreateUseCase();
//         var input = GetValidInput();
//         var encryptedPassword = $"enc_{input.Password}";
//
//         PasswordEncripterMock
//             .Setup(x => x.Encrypt(input.Password))
//             .Returns(encryptedPassword);
//
//         CustomerRepositoryMock
//             .Setup(x => x.GetByEmail(input.Email, It.IsAny<CancellationToken>()))
//             .ReturnsAsync((Customer?)null);
//
//         CustomerRepositoryMock
//             .Setup(x => x.Insert(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
//             .ThrowsAsync(new Exception("Erro de banco"));
//
//         SetupUnitOfWorkMock();
//
//         // Act
//         var result = await useCase.Execute(input);
//
//         // Assert
//         result.IsSuccess.Should().BeFalse();
//         result.Error.GetErrors().Should().ContainSingle()
//             .Which.Message.Should().Be("Erro de banco");
//     }
//
//     [Fact(DisplayName = "Não deve criar cliente com senha fraca")]
//     public async Task NaoDeveCriarClienteComSenhaFraca()
//     {
//         // Arrange
//         var useCase = CreateUseCase();
//         var input = new CreateCustomerInput(GetValidName(), GetValidEmail(), "abcdef");
//
//         CustomerRepositoryMock
//             .Setup(x => x.GetByEmail(input.Email, It.IsAny<CancellationToken>()))
//             .ReturnsAsync((Customer?)null);
//
//         PasswordEncripterMock
//             .Setup(x => x.Encrypt(input.Password))
//             .Returns($"enc_{input.Password}");
//
//         SetupUnitOfWorkMock();
//
//         // Act
//         var result = await useCase.Execute(input);
//
//         // Assert
//         result.IsSuccess.Should().BeFalse();
//         var errors = result.Error.GetErrors();
//         errors.Should().Contain(e => e.Message.Contains("'senha' deve conter ao menos uma letra maiúscula."));
//         errors.Should().Contain(e => e.Message.Contains("'senha' deve conter ao menos um número."));
//     }
// }



 