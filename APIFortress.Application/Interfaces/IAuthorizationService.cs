namespace ApiFortress.Application.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeUserAsync(int userId, string requiredPermission);
    }
}