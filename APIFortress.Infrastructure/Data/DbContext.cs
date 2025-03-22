using Microsoft.EntityFrameworkCore;
using ApiFortress.Domain.Entities;

namespace ApiFortress.Infrastructure.Data
{
    public class ApiFortressDbContext : DbContext
    {
        public ApiFortressDbContext(DbContextOptions<ApiFortressDbContext> options)
            : base(options)
        {
        }
        public DbSet<APIUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}