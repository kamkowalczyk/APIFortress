using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ApiFortress.Application.DTOs;
using ApiFortress.Application.Interfaces;
using ApiFortress.Infrastructure.Repositories.Interfaces;
using ApiFortress.Infrastructure.Providers;

namespace ApiFortress.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTTokenProvider _jwtTokenProvider;
        private readonly IAuditService _auditService;

        public AuthService(IUserRepository userRepository, JWTTokenProvider jwtTokenProvider, IAuditService auditService)
        {
            _userRepository = userRepository;
            _jwtTokenProvider = jwtTokenProvider;
            _auditService = auditService;
        }

        public async Task<LoginResponseDTO> AuthenticateUserAsync(LoginRequestDTO loginRequest)
        {
            var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
            if (user == null || user.PublicKey != loginRequest.PublicKey)
                return null;

            var mainRole = user.Roles.FirstOrDefault()?.Name;

            string accessToken = _jwtTokenProvider.GenerateToken(user.Id, user.Username, mainRole);
            string refreshToken = _jwtTokenProvider.GenerateToken(user.Id, user.Username, mainRole);

            await _auditService.LogEventAsync(user.Id, "User logged in");

            return new LoginResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = 3600
            };
        }

        public async Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUser)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(registerUser.Username);
            if (existingUser != null)
                return null;

            var newUser = new Domain.Entities.APIUser
            {
                Username = registerUser.Username,
                PublicKey = registerUser.PublicKey
            };

            await _userRepository.AddAsync(newUser);
            await _auditService.LogEventAsync(newUser.Id, "User registered");

            var userDto = new UserDTO
            {
                Id = newUser.Id,
                Username = newUser.Username
            };

            return userDto;
        }

        public async Task LogoutAsync(int userId)
        {
            await _auditService.LogEventAsync(userId, "User logged out");
            await Task.CompletedTask;
        }

        public async Task<LoginResponseDTO> RefreshTokenAsync(RefreshTokenDTO tokenDto)
        {
 
                var principal = _jwtTokenProvider.ValidateToken(tokenDto.RefreshToken);
                if (principal == null)
                    return null;

                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)
                                  ?? principal.FindFirst(JwtRegisteredClaimNames.Sub);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return null;

                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                    return null;

                string newAccessToken = _jwtTokenProvider.GenerateToken(user.Id, user.Username);
                string newRefreshToken = _jwtTokenProvider.GenerateToken(user.Id, user.Username);

            await _auditService.LogEventAsync(user.Id, "Token refreshed");

            return new LoginResponseDTO
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    ExpiresIn = 3600
                };
        }
    }
}
