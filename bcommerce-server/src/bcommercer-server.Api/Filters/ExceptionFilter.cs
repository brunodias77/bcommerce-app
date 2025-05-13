using System.Net;
using bcommerce_server.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bcommercer_server.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BcommerceExceptionBase)
        {
            HandlerProjectException(context);
        }
        else
        {
            ThrowUnknowException(context);
        }    
    }

    private void HandlerProjectException(ExceptionContext context)
    {
        
    }

    private void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new
        {
            message = "Erro desconhecido!"
        });
    }

}