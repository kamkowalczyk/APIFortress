using Microsoft.AspNetCore.Http;
using ApiFortress.Infrastructure.Providers;

namespace ApiFortress.Infrastructure.Middleware
{
    public class IPBlockMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IPBlocker _ipBlocker;

        public IPBlockMiddleware(RequestDelegate next, IPBlocker ipBlocker)
        {
            _next = next;
            _ipBlocker = ipBlocker;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            if (_ipBlocker.IsIPBlocked(ipAddress))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Your IP is blocked.");
                return;
            }
            await _next(context);
        }
    }
}
