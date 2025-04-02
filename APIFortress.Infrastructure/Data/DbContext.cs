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
        public DbSet<AuditDetails> AuditDetails { get; set; }

        public DbSet<DataItem> DataItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Administrator" },
                new Role { Id = 2, Name = "User", Description = "User" }
            );

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Name = "CanRead", Description = "Permission to read data" },
                new Permission { Id = 2, Name = "CanWrite", Description = "Permission to write data" },
                new Permission { Id = 3, Name = "CanDelete", Description = "Permission to delete data" }
            );

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity(j => j.HasData(
                    new { RolesId = 1, PermissionsId = 1 },
                    new { RolesId = 1, PermissionsId = 2 },
                    new { RolesId = 1, PermissionsId = 3 },
                    new { RolesId = 2, PermissionsId = 1 }
                ));

            modelBuilder.Entity<APIUser>().HasData(
            new APIUser { Id = 1, Username = "admin", PublicKey = "AdminKey_ABC123" },
            new APIUser { Id = 2, Username = "user1", PublicKey = "User1Key_DEF456" },
            new APIUser { Id = 3, Username = "user2", PublicKey = "User2Key_GHI789" },
            new APIUser { Id = 4, Username = "user3", PublicKey = "User3Key_JKL012" },
            new APIUser { Id = 5, Username = "user4", PublicKey = "User4Key_MNO345" }
        );

            modelBuilder.Entity<APIUser>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.HasData(
                    new { UsersId = 1, RolesId = 1 },
                    new { UsersId = 2, RolesId = 2 },
                    new { UsersId = 3, RolesId = 2 },
                    new { UsersId = 4, RolesId = 2 },
                    new { UsersId = 5, RolesId = 2 }
                ));

            modelBuilder.Entity<DataItem>().HasData(
            new DataItem { Id = 1, Title = "Security Policy", Description = "Current API security policies and guidelines." },
            new DataItem { Id = 2, Title = "Incident Report", Description = "Report detailing recent unauthorized access attempts." },
            new DataItem { Id = 3, Title = "Audit Summary", Description = "Summary of recent audit logs." },
            new DataItem { Id = 4, Title = "User Access Report", Description = "Overview of user access levels and activity." },
            new DataItem { Id = 5, Title = "System Metrics", Description = "Current system performance metrics and statistics." }
        );
        }
    }
}