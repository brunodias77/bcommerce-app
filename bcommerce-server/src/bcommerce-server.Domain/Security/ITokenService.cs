using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcommerce_server.Domain.Security
{
    public interface ITokenService
    {
        public string GenerateToken(Guid userId);

        public Guid ValidateAndGetUserIdentifier(string token);


    }
}