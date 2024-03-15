using LingoLabs.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LingoLabs.Identity.Data
{
    public class LingoLabsIdentityDbContext: IdentityDbContext<ApplicationUser>
    {
        public LingoLabsIdentityDbContext(DbContextOptions<LingoLabsIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            var adminUserId = Guid.NewGuid().ToString();
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var password = passwordHasher.HashPassword(null, "AdminPassword@1");
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                FirstName = "admin",
                LastName = "admin",
                PhoneNumber = "0000000000",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM"
            });

            builder.Entity<ApplicationUser>().HasKey(x => new { x.Id });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = adminUserId,
                RoleId = adminRoleId
            });
            try
            {
                SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
