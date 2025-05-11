using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Services;
using bcommerce_server.Infra.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace bcommerce_server.Infra.Services;

public class LoggedCustomer : ILoggedCustomer
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;
    private readonly ICustomerRepository _customerRepository;

    public LoggedCustomer(
        IUnitOfWork unitOfWork,
        ITokenProvider tokenProvider,
        ICustomerRepository customerRepository)
    {
        _unitOfWork = unitOfWork;
        _tokenProvider = tokenProvider;
        _customerRepository = customerRepository;
    }

    public async Task<Customer> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var id = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var customerID = Guid.Parse(id);
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new SecurityTokenException("ID do cliente inválido ou ausente no token.");
        }

        var customer = await _customerRepository.Get(customerID, CancellationToken.None);
        if (customer == null)
        {
            throw new UnauthorizedAccessException("Cliente não encontrado.");
        }

        return customer;
    }

    private async Task BeginTransaction()
    {
        await _unitOfWork.Begin();
    }
}

// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using bcommerce_server.Domain.Customers;
// using bcommerce_server.Domain.Customers.Repositories;
// using bcommerce_server.Domain.Services;
// using bcommerce_server.Infra.Repositories;
// using Microsoft.IdentityModel.Tokens;
//
// namespace bcommerce_server.Infra.Services;
//
// public class LoggedCustomer : ILoggedCustomer
// {
//     public LoggedCustomer(IUnitOfWork unitOfWork, ITokenProvider tokenProvider, ICustomerRepository customerRepository)
//     {
//         _unitOfWork = unitOfWork;
//         _tokenProvider = tokenProvider;
//         _customerRepository = customerRepository;
//     }
//
//     private readonly IUnitOfWork _unitOfWork;
//     private readonly ITokenProvider _tokenProvider;
//     private readonly ICustomerRepository _customerRepository;
//     
//     public async Task<Customer> User()
//     {
//         var token = _tokenProvider.Value();
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
//         var id = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
//         if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out Guid customerId))
//         {
//             throw new SecurityTokenException("ID do cliente inválido no token.");
//         }
//         try
//         {
//             await BeginTransaction();
//             var customer = await _customerRepository.Get(customerId, CancellationToken.None);
//             if (customer == null)
//             {
//                 throw new UnauthorizedAccessException("Cliente não encontrado.");
//             }
//             return customer;
//         }
//         catch (Exception ex)
//         {
//             throw new ApplicationException("Falha ao obter cliente logado.", ex);
//         }
//     }
//     
//     private async Task BeginTransaction()
//     {
//         await _unitOfWork.Begin();
//     }
// }