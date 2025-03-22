using ApiFortress.Domain.Entities;
using ApiFortress.Infrastructure.Repositories.Interfaces;
namespace ApiFortress.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<APIUser> _users = new List<APIUser>();

        public Task AddAsync(APIUser user)
        {
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
                _users.Remove(user);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<APIUser>> GetAllAsync()
        {
            return Task.FromResult(_users.AsEnumerable());
        }

        public Task<APIUser> GetByIdAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<APIUser> GetByUsernameAsync(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            return Task.FromResult(user);
        }

        public Task UpdateAsync(APIUser user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.PublicKey = user.PublicKey;
                existingUser.Roles = user.Roles;
                existingUser.Permissions = user.Permissions;
            }
            return Task.CompletedTask;
        }
    }
}
