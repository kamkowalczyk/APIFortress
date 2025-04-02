using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiFortress.Application.Interfaces;
using ApiFortress.Application.DTOs;
using ApiFortress.Infrastructure.Repositories.Interfaces;

namespace ApiiFortress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAuditService _auditService;
        private readonly IUserRepository _userRepository;

        public AdminController(IAuditService auditService, IUserRepository userRepository)
        {
            _auditService = auditService;
            _userRepository = userRepository;
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username
            });
            return Ok(userDtos);
        }

        [HttpGet("Logs")]
        public async Task<IActionResult> GetAuditLogs()
        {
            var logs = await _auditService.GetAuditLogsAsync();
            return Ok(logs);
        }
    }
}