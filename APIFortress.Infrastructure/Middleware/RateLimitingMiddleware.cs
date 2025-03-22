using Microsoft.AspNetCore.Http;
using ApiFortress.Infrastructure.Providers;

namespace ApiFortress.Infrastructure.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RateLimiter _rateLimiter;

        public RateLimitingMiddleware(RequestDelegate next, RateLimiter rateLimiter)
        {
            _next = next;
            _rateLimiter = rateLimiter;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            if (!_rateLimiter.IsRequestAllowed(ipAddress))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too Many Requests");
                return;
            }
            await _next(context);
        }
    }
}