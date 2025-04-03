using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ApiFortress.Infrastructure.Middleware
{
    public class AnomalyDetectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AnomalyDetectionMiddleware> _logger;
        private readonly int _responseTimeThreshold;

        public AnomalyDetectionMiddleware(RequestDelegate next, ILogger<AnomalyDetectionMiddleware> logger, int responseTimeThreshold = 1000)
        {
            _next = next;
            _logger = logger;
            _responseTimeThreshold = responseTimeThreshold;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > _responseTimeThreshold)
            {
                _logger.LogWarning("High response time detected: {ElapsedMilliseconds} ms for request {Method} {Path}",
                    stopwatch.ElapsedMilliseconds, context.Request.Method, context.Request.Path);
            }
        }
    }
}