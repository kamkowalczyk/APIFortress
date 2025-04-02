using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ApiFortress.Infrastructure.Providers
{
    public class JWTTokenProvider
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationInMinutes;

        public JWTTokenProvider(string secret, string issuer, string audience, int expirationInMinutes)
        {
            _secret = secret;
            _issuer = issuer;
            _audience = audience;
            _expirationInMinutes = expirationInMinutes;
        }
        public string GenerateToken(int userId, string username, string role = null)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Invalid user data provided for token generation.");

            if (string.IsNullOrWhiteSpace(_secret))
                throw new InvalidOperationException("JWT Secret is not configured.");

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var claimsList = new List<Claim>(claims);
                if (!string.IsNullOrWhiteSpace(role))
                {
                    claimsList.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    claims: claimsList,
                    expires: DateTime.UtcNow.AddMinutes(_expirationInMinutes),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating JWT token: " + ex.Message, ex);
            }
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secret);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
    }
}