using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiiFortress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class MetricsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMetrics()
        {
            return Ok(new
            {
                uptime = "72 hours",
                requestCount = 1234,
                averageResponseTime = "200ms"
            });
        }
    }
}