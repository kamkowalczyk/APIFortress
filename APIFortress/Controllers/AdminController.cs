using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiFortress.Application.Interfaces;

namespace ApiiFortress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAuditService _auditService;

        public AdminController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(new { message = "List of users" });
        }

        [HttpGet("logs")]
        public async Task<IActionResult> GetAuditLogs()
        {
            var logs = await _auditService.GetAuditLogsAsync();
            return Ok(logs);
        }
    }
}
