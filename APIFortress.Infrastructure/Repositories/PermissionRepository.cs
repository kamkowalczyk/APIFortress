using ApiFortress.Domain.Entities;
using ApiFortress.Infrastructure.Repositories.Interfaces;

namespace ApiFortress.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly List<Permission> _permissions = new List<Permission>();

        public Task AddAsync(Permission permission)
        {
            permission.Id = _permissions.Count > 0 ? _permissions.Max(p => p.Id) + 1 : 1;
            _permissions.Add(permission);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var permission = _permissions.FirstOrDefault(p => p.Id == id);
            if (permission != null)
                _permissions.Remove(permission);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Permission>> GetAllAsync()
        {
            return Task.FromResult(_permissions.AsEnumerable());
        }

        public Task<Permission> GetByIdAsync(int id)
        {
            var permission = _permissions.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(permission);
        }

        public Task<Permission> GetByNameAsync(string name)
        {
            var permission = _permissions.FirstOrDefault(p => p.Name == name);
            return Task.FromResult(permission);
        }

        public Task UpdateAsync(Permission permission)
        {
            var existingPermission = _permissions.FirstOrDefault(p => p.Id == permission.Id);
            if (existingPermission != null)
            {
                existingPermission.Name = permission.Name;
                existingPermission.Description = permission.Description;
            }
            return Task.CompletedTask;
        }
    }
}