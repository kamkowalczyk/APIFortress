using Microsoft.AspNetCore.Http;
using ApiFortress.Infrastructure.Providers;

namespace ApiFortress.Infrastructure.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTTokenProvider _tokenProvider;

        public JwtMiddleware(RequestDelegate next, JWTTokenProvider tokenProvider)
        {
            _next = next;
            _tokenProvider = tokenProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Split(" ").Last();
                try
                {
                    var principal = _tokenProvider.ValidateToken(token);
                    context.User = principal;
                }
                catch
                {
                }
            }
            await _next(context);
        }
    }
}