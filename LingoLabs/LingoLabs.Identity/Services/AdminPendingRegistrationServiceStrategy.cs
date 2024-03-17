using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Application.Models.Identity;
using LingoLabs.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace LingoLabs.Identity.Services
{
    public class AdminPendingRegistrationServiceStrategy: IRegistrationServiceStrategy
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminPendingRegistrationServiceStrategy(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<(int status, string message)> Registration(RegistrationModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email!);
            if (userExists != null)
                return (UserAuthenticationStatus.REGISTRATION_FAIL, "Email already used");

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (UserAuthenticationStatus.REGISTRATION_FAIL, "User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync("AdminPending"))
                await roleManager.CreateAsync(new IdentityRole("AdminPending"));

            if (await roleManager.RoleExistsAsync("AdminPending"))
                await userManager.AddToRoleAsync(user, "AdminPending");

            return (UserAuthenticationStatus.REGISTRATION_SUCCES, "User created successfully!");
        }
    }
}
