using Microsoft.EntityFrameworkCore;
namespace MembershipAPI.models
{
    public class GlobalMembershipDbContext : DbContext
    {
        public GlobalMembershipDbContext(DbContextOptions<GlobalMembershipDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserApplication> UserApplications { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

       
    }
}
