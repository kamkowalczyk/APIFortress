using Microsoft.AspNetCore.Mvc;
using ApiFortress.Application.DTOs;
using ApiFortress.Application.Interfaces;

namespace ApiiFortress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var result = await _authService.AuthenticateUserAsync(loginRequest);
            if (result == null)
                return Unauthorized("Invalid credentials");
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUser)
        {
            var result = await _authService.RegisterUserAsync(registerUser);
            return Ok(result);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDTO tokenDto)
        {
            var result = await _authService.RefreshTokenAsync(tokenDto);
            if (result == null)
                return Unauthorized("Invalid refresh token");
            return Ok(result);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromQuery] int userId)
        {
            await _authService.LogoutAsync(userId);
            return Ok("Logged out successfully");
        }
    }
}