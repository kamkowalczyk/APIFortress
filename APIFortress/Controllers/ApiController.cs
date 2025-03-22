using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiiFortress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {
        [HttpGet("data")]
        public IActionResult GetData()
        {
            return Ok(new { message = "This is protected data" });
        }

        [HttpPost("data")]
        public IActionResult PostData([FromBody] object data)
        {
            return Ok(new { message = "Data processed successfully" });
        }
    }
}