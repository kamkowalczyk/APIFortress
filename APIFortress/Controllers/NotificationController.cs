﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiiFortress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class NotificationController : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] string message)
        {
            await Task.CompletedTask;
            return Ok(new { status = "Notification sent", message });
        }

        [HttpGet("status")]
        public IActionResult GetNotificationStatus()
        {
            return Ok(new { status = "All notifications delivered" });
        }
    }
}