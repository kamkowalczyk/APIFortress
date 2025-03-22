using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace ApiFortress.Infrastructure.Helpers
{
    public static class JwtHelper
    {
        public static string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == JwtRegisteredClaimNames.Sub);
            return userIdClaim?.Value;
        }
    }
}