using ApiFortress.Domain.Entities;

namespace ApiFortress.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<APIUser> GetByIdAsync(int id);
        Task<APIUser> GetByUsernameAsync(string username);
        Task AddAsync(APIUser user);
        Task UpdateAsync(APIUser user);
        Task DeleteAsync(int id);
        Task<IEnumerable<APIUser>> GetAllAsync();
    }
}