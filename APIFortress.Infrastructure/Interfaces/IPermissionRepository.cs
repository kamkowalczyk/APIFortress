using ApiFortress.Domain.Entities;

namespace ApiFortress.Infrastructure.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        Task<Permission> GetByIdAsync(int id);
        Task<Permission> GetByNameAsync(string name);
        Task AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task DeleteAsync(int id);
        Task<IEnumerable<Permission>> GetAllAsync();
    }
}
