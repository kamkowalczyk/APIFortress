using ApiFortress.Application.DTOs;
using ApiFortress.Application.Interfaces;

namespace ApiFortress.Application.Services
{
    public class AuthService : IAuthService
    {

        public async Task<LoginResponseDTO> AuthenticateUserAsync(LoginRequestDTO loginRequest)
        {
            var response = new LoginResponseDTO
            {
                AccessToken = "dummy-access-token",
                RefreshToken = "dummy-refresh-token",
                ExpiresIn = 3600
            };
            return await Task.FromResult(response);
        }

        public async Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUser)
        {
            var user = new UserDTO
            {
                Id = 1,
                Username = registerUser.Username
            };
            return await Task.FromResult(user);
        }

        public async Task LogoutAsync(int userId)
        {
            await Task.CompletedTask;
        }

        public async Task<LoginResponseDTO> RefreshTokenAsync(string refreshToken)
        {
            var response = new LoginResponseDTO
            {
                AccessToken = "new-dummy-access-token",
                RefreshToken = "new-dummy-refresh-token",
                ExpiresIn = 3600
            };
            return await Task.FromResult(response);
        }
    }
}