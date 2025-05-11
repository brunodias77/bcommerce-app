using System.Net;

namespace bcommerce_server.Domain.Exceptions;

public class UnauthorizedException : BcommerceExceptionBase
{
    public UnauthorizedException(string message) : base(message)
    {
    }
    
    public override IList<string> GetErrorMessages()
    {
        throw new NotImplementedException();
    }

    public override HttpStatusCode GetStatusCode()
    {
        throw new NotImplementedException();
    }
}