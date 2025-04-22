using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcommerce_server.Domain.Security
{
    public class JwtSettings
    {
        public string SigninKey { get; set; } = null!;
        public int ExpirationTimeMinutes { get; set; }
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}