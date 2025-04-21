using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcommerce_server.Domain.Security;
using BC = BCrypt.Net.BCrypt;

namespace bcommerce_server.Infra.Security
{
    public class PasswordEncripter : IPasswordEncripter
    {
        public string Encrypt(string password)
        {
            string passwordHash = BC.HashPassword(password);

            return passwordHash;
        }

        public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
    }
}