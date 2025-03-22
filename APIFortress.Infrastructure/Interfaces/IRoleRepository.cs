using ApiFortress.Domain.Entities;

namespace ApiFortress.Infrastructure.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetByIdAsync(int id);
        Task<Role> GetByNameAsync(string name);
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(int id);
        Task<IEnumerable<Role>> GetAllAsync();
    }
}