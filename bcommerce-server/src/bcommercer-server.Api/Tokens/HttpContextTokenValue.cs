using bcommerce_server.Domain.Services;

namespace bcommercer_server.Api.Tokens;

public class HttpContextTokenValue(IHttpContextAccessor _httpContextAccessor) : ITokenProvider
{
    public string Value()
    {
        var authentication = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authentication["Bearer ".Length..].ToString();
    }
}