using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Exceptions;
using bcommerce_server.Domain.Security;
using bcommerce_server.Infra.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bcommercer_server.Api.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    public AuthenticatedUserFilter(ICustomerRepository customerRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    private readonly ICustomerRepository _customerRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;



    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = GetTokenFromRequest(context);
        var userID = _tokenService.ValidateAndGetUserIdentifier(token);
        // verificar se esse id existe no banco de dados
        await _unitOfWork.Begin();
        var userExists = await _customerRepository.Get(userID, CancellationToken.None);
        if(userExists is null)
            throw new UnauthorizedException("Usuario nao identificado");
    }


    private static string GetTokenFromRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authentication))
            throw new UnauthorizedException("a requisicao nao veio com token");

        return authentication["Bearer ".Length..].Trim();
    }
}