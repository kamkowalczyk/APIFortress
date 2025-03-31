using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiiFortress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {
        [HttpGet("Data")]
        public IActionResult GetData()
        {
            return Ok(new { message = "This is protected data" });
        }

        [HttpPost("Data")]
        public IActionResult PostData([FromBody] object data)
        {
            return Ok(new { message = "Data processed successfully" });
        }
    }
}