using ApiFortress.Domain.Entities;
using ApiFortress.Infrastructure.Repositories.Interfaces;
using ApiFortress.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiFortress.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiFortressDbContext _context;

        public UserRepository(ApiFortressDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(APIUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<APIUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<APIUser> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<APIUser> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task UpdateAsync(APIUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
