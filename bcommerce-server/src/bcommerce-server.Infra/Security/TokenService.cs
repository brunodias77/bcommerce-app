using System.IdentityModel.Tokens.Jwt;
using bcommerce_server.Domain.Security;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace bcommerce_server.Infra.Security
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;

        public TokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public string GenerateToken(Guid userId)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, userId.ToString())
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_settings.ExpirationTimeMinutes),
                SigningCredentials =
                    new SigningCredentials(SecurityKey(_settings.SigninKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public Guid ValidateAndGetUserIdentifier(string token)
        {
            var validationParameter = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = SecurityKey(_settings.SigninKey),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, validationParameter, out _);
            var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            return Guid.Parse(userIdentifier);
        }

        private static SymmetricSecurityKey SecurityKey(string key)
        {
            var bytes = Encoding.UTF8.GetBytes(key);
            return new SymmetricSecurityKey(bytes);
        }
    }
}