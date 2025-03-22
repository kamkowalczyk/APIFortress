using ApiFortress.Domain.Entities;
using ApiFortress.Infrastructure.Repositories.Interfaces;

namespace ApiFortress.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly List<Role> _roles = new List<Role>();

        public Task AddAsync(Role role)
        {
            role.Id = _roles.Count > 0 ? _roles.Max(r => r.Id) + 1 : 1;
            _roles.Add(role);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var role = _roles.FirstOrDefault(r => r.Id == id);
            if (role != null)
                _roles.Remove(role);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Role>> GetAllAsync()
        {
            return Task.FromResult(_roles.AsEnumerable());
        }

        public Task<Role> GetByIdAsync(int id)
        {
            var role = _roles.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(role);
        }

        public Task<Role> GetByNameAsync(string name)
        {
            var role = _roles.FirstOrDefault(r => r.Name == name);
            return Task.FromResult(role);
        }

        public Task UpdateAsync(Role role)
        {
            var existingRole = _roles.FirstOrDefault(r => r.Id == role.Id);
            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                existingRole.Description = role.Description;
                existingRole.Permissions = role.Permissions;
            }
            return Task.CompletedTask;
        }
    }
}