using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bcommerce_server.Domain.Security
{
    public class JwtSettings
    {
        public uint ExpirationTimeMinutes { get; set; }
        public string SigninKey { get; set; } = string.Empty;
    }
}