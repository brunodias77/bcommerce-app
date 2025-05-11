using bcommercer_server.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace bcommercer_server.Api.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}