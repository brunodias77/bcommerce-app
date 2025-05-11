using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using bcommerce_server.Domain.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace bcommerce_server.Infra.Security
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;
        private readonly SymmetricSecurityKey _securityKey;

        public TokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(_settings.SigninKey))
                throw new ArgumentException("SigninKey está vazio ou nulo nas configurações do JWT.");

            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SigninKey));
        }

        public string GenerateToken(Guid userId)
        {
            var claims = new List<Claim>
            {
                new("sub", userId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_settings.ExpirationTimeMinutes),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        
        public Guid ValidateAndGetUserIdentifier(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                var userIdClaim = principal.FindFirst("sub") ?? principal.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                    throw new SecurityTokenException("Claim de identificação do usuário não encontrada.");

                return Guid.Parse(userIdClaim.Value);
            }
            catch (Exception)
            {
                throw new SecurityTokenException("Token inválido.");
            }
        }

        // public Guid ValidateAndGetUserIdentifier(string token)
        // {
        //     var validationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuer = true,
        //         ValidateAudience = true,
        //         ValidIssuer = _settings.Issuer,
        //         ValidAudience = _settings.Audience,
        //         ValidateIssuerSigningKey = true,
        //         IssuerSigningKey = _securityKey,
        //         ValidateLifetime = true,
        //         ClockSkew = TimeSpan.Zero
        //     };
        //
        //     try
        //     {
        //         var tokenHandler = new JwtSecurityTokenHandler();
        //         var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        //
        //         var userId = principal.Claims.First(c => c.Type == "sub").Value;
        //         return Guid.Parse(userId);
        //     }
        //     catch
        //     {
        //         throw new SecurityTokenException("Invalid token");
        //     }
        // }
    }
}


//using System.IdentityModel.Tokens.Jwt; 
// using bcommerce_server.Domain.Security;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.Extensions.Options;
// using Microsoft.IdentityModel.Tokens;

// namespace bcommerce_server.Infra.Security
// {
//     public class TokenService : ITokenService
//     {
//         private readonly JwtSettings _settings;

//         public TokenService(IOptions<JwtSettings> options)
//         {
//             _settings = options.Value;
//         }

//         public string GenerateToken(Guid userId)
//         {
//             var claims = new List<Claim>
//         {
//             new(ClaimTypes.Sid, userId.ToString())
//         };

//             var tokenDescriptor = new SecurityTokenDescriptor
//             {
//                 Subject = new ClaimsIdentity(claims),
//                 Expires = DateTime.UtcNow.AddMinutes(_settings.ExpirationTimeMinutes),
//                 SigningCredentials =
//                     new SigningCredentials(SecurityKey(_settings.SigninKey), SecurityAlgorithms.HmacSha256Signature)
//             };

//             var tokenHandler = new JwtSecurityTokenHandler();
//             var token = tokenHandler.CreateToken(tokenDescriptor);

//             return tokenHandler.WriteToken(token);
//         }

//         public Guid ValidateAndGetUserIdentifier(string token)
//         {
//             var validationParameter = new TokenValidationParameters
//             {
//                 ValidateIssuer = false,
//                 ValidateAudience = false,
//                 IssuerSigningKey = SecurityKey(_settings.SigninKey),
//                 ClockSkew = TimeSpan.Zero
//             };

//             var tokenHandler = new JwtSecurityTokenHandler();
//             var principal = tokenHandler.ValidateToken(token, validationParameter, out _);
//             var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

//             return Guid.Parse(userIdentifier);
//         }

//         private static SymmetricSecurityKey SecurityKey(string key)
//         {
//             var bytes = Encoding.UTF8.GetBytes(key);
//             return new SymmetricSecurityKey(bytes);
//         }
//     }
// }