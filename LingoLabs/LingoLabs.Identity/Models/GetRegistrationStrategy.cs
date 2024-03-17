using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Identity.Services;
using Microsoft.AspNetCore.Identity;

namespace LingoLabs.Identity.Models
{
    public class GetRegistrationStrategy
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public GetRegistrationStrategy(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IRegistrationServiceStrategy GetRegistrationRoleStrategy(string role)
        {
            switch (role)
            {
                case UserRoles.Student:
                    return new StudentRegistrationServiceStrategy(userManager, roleManager);
                case UserRoles.Admin:
                    return new AdminPendingRegistrationServiceStrategy(userManager, roleManager);
                default:
                    return new InvalidRoleRegistrationServiceStrategy();
            }
        }
    }
}
