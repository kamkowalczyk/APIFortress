using ApiFortress.Application.Interfaces;

namespace ApiFortress.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {

        public async Task<bool> AuthorizeUserAsync(int userId, string requiredPermission)
        {
            return await Task.FromResult(true);
        }
    }
}
