using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiFortress.Infrastructure.Services;
using System;

namespace ApiFortress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class MetricsController : ControllerBase
    {
        private readonly MetricsTracker _metricsTracker;

        public MetricsController(MetricsTracker metricsTracker)
        {
            _metricsTracker = metricsTracker;
        }

        [HttpGet]
        public IActionResult GetMetrics()
        {
            var uptime = DateTime.UtcNow - _metricsTracker.AppStartTime;
            return Ok(new
            {
                uptime = uptime.ToString(@"dd\.hh\:mm\:ss"),
                requestCount = _metricsTracker.RequestCount,
                averageResponseTime = "200ms" 
            });
        }
    }
}