using System;
using System.Threading;
using System.Threading.Tasks;
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Security;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.Domain.Validations;
using bcommerce_server.Infra.Repositories;

namespace bcommerce_server.Application.Customers.Login;

public class LoginCustomerUseCase : ILoginCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly ITokenService _tokenService;

    public LoginCustomerUseCase(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IPasswordEncripter passwordEncripter,
        ITokenService tokenService)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginCustomerOutput, Notification>> Execute(LoginCustomerInput input)
    {
        try
        {
            await Begin();

            var customer = await GetCustomerByEmail(input.Email);

            if (customer is null || !IsPasswordValid(input.Password, customer.Password))
                return Fail("Email ou senha inválidos.");

            var token = GenerateToken(customer);
            return Success(token, customer.Name);
        }
        catch (System.Exception ex) 
        {
            return Fail(ex.Message);
        }
        finally
        {
            await _unitOfWork.DisposeAsync();
        }
    }

    // 🔽 Métodos privados expressivos

    private async Task Begin()
    {
        await _unitOfWork.Begin();
    }

    private async Task<Customer?> GetCustomerByEmail(string email)
    {
        return await _customerRepository.GetByEmail(email, CancellationToken.None);
    }

    private bool IsPasswordValid(string inputPassword, string hashedPassword)
    {
        return _passwordEncripter.Verify(inputPassword, hashedPassword);
    }

    private string GenerateToken(Customer customer)
    {
        return _tokenService.GenerateToken(customer.Id.Value);
    }

    private Result<LoginCustomerOutput, Notification> Success(string token, string name)
    {
        return Result<LoginCustomerOutput, Notification>.Ok(new LoginCustomerOutput(token, name));
    }

    private Result<LoginCustomerOutput, Notification> Fail(string message)
    {
        return Result<LoginCustomerOutput, Notification>.Fail(Notification.Create(new Error(message)));
    }
}



// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using bcommerce_server.Application.Abstractions;
// using bcommerce_server.Domain.Customers;
// using bcommerce_server.Domain.Customers.Repositories;
// using bcommerce_server.Domain.Security;
// using bcommerce_server.Domain.Validations.Handlers;
// using bcommerce_server.Domain.Validations;
// using bcommerce_server.Infra.Repositories;
//
// namespace bcommerce_server.Application.Customers.Login;
//
// public class LoginCustomerUseCase : ILoginCustomerUseCase
// {
//     public LoginCustomerUseCase(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IPasswordEncripter passwordEncripter, ITokenService tokenService)
//     {
//         _customerRepository = customerRepository;
//         _unitOfWork = unitOfWork;
//         _passwordEncripter = passwordEncripter;
//         _tokenService = tokenService;
//     }
//
//     private readonly ICustomerRepository _customerRepository;
//     private readonly IUnitOfWork _unitOfWork;
//     private readonly IPasswordEncripter _passwordEncripter;
//     private readonly ITokenService _tokenService;
//
//     
//     public async Task<Result<LoginCustomerOutput, Notification>> Execute(LoginCustomerInput input)
//     {
//         // Pegar o customer no banco pelo email
//         var customerExists = await GetByEmail(input.Email);
//         // verificar se as senhas batem 
//         var passwordMatch = _passwordEncripter.Verify(input.Password, customerExists.Password);
//         // verificar se o customer e null se e passwordMatch e false
//         if (customerExists is null || !passwordMatch)
//         {
//             return Result<LoginCustomerOutput, Notification>.Fail(Notification.Create(new Error("Email ou senha invalidas")));
//
//         }
//         // criar um token jwt
//         var token = _tokenService.GenerateToken(customerExists.Id.Value); // ✅ gera o JWT
//
//         // mandar a resposta
//
//         return Result<LoginCustomerOutput, Notification>.Ok(new LoginCustomerOutput(token));
//     }
//     
//     private async Task BeginTransaction()
//     {
//         await _unitOfWork.Begin();
//     }
//     
//     private async Task<Customer> GetByEmail(string email)
//     {
//         var customer = await _customerRepository.GetByEmail(email, CancellationToken.None);
//         return customer;
//     }
// }