using ApiFortress.Application.DTOs;

namespace ApiFortress.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> AuthenticateUserAsync(LoginRequestDTO loginRequest);
        Task<UserDTO> RegisterUserAsync(RegisterUserDTO registerUser);
        Task LogoutAsync(int userId);
        Task<LoginResponseDTO> RefreshTokenAsync(string refreshToken);
    }
}