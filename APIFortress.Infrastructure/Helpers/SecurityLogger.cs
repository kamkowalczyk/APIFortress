using Microsoft.Extensions.Logging;

namespace ApiFortress.Infrastructure.Helpers
{
    public class SecurityLogger
    {
        private readonly ILogger<SecurityLogger> _logger;

        public SecurityLogger(ILogger<SecurityLogger> logger)
        {
            _logger = logger;
        }

        public void LogSecurityEvent(string message)
        {
            _logger.LogInformation("Security Event: {Message}", message);
        }
    }
}