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

        public AuthService(IUserRepository userRepository, JWTTokenProvider jwtTokenProvider)
        {
            _userRepository = userRepository;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task<LoginResponseDTO> AuthenticateUserAsync(LoginRequestDTO loginRequest)
        {
            var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
            if (user == null || user.PublicKey != loginRequest.PublicKey)
                return null;

            string accessToken = _jwtTokenProvider.GenerateToken(user.Id, user.Username);
            string refreshToken = _jwtTokenProvider.GenerateToken(user.Id, user.Username);

            var response = new LoginResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = 3600
            };

            return response;
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
                // Tutaj możesz dodać inne właściwości, np. Email, Forename, Surename, jeśli encja APIUser je posiada.
            };

            await _userRepository.AddAsync(newUser);

            var userDto = new UserDTO
            {
                Id = newUser.Id,
                Username = newUser.Username
            };

            return userDto;
        }

        public async Task LogoutAsync(int userId)
        {
            // Implementacja wylogowania – np. unieważnienie refresh tokena.
            await Task.CompletedTask;
        }

        public async Task<LoginResponseDTO> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                // Weryfikacja refresh tokena, która zwraca ClaimsPrincipal, jeżeli token jest poprawny
                var principal = _jwtTokenProvider.ValidateToken(refreshToken);
                if (principal == null)
                    return null;

                // Odczytujemy identyfikator użytkownika z tokena (claim "sub" lub ClaimTypes.NameIdentifier)
                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)
                                  ?? principal.FindFirst(JwtRegisteredClaimNames.Sub);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return null;

                // Pobieramy użytkownika z bazy danych
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                    return null;

                // Generujemy nowe tokeny na podstawie aktualnych danych użytkownika
                string newAccessToken = _jwtTokenProvider.GenerateToken(user.Id, user.Username);
                string newRefreshToken = _jwtTokenProvider.GenerateToken(user.Id, user.Username);

                return new LoginResponseDTO
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    ExpiresIn = 3600
                };
            }
            catch (Exception ex)
            {
                // Warto zalogować wyjątek dla celów diagnostycznych
                return null;
            }
        }
    }
}
