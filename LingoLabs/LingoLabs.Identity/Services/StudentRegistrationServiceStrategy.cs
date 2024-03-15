using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Application.Models.Identity;
using LingoLabs.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace LingoLabs.Identity.Services
{
    public class StudentRegistrationServiceStrategy : IRegistrationServiceStrategy
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public StudentRegistrationServiceStrategy(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<(int status, string message)> Registration(RegistrationModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email!);
            if (userExists != null)
                return (UserAuthenticationStatus.REGISTRATION_FAIL, "Username already used");

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

            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));

            if (await roleManager.RoleExistsAsync(UserRoles.Student))
                await userManager.AddToRoleAsync(user, model.Role);

            return (UserAuthenticationStatus.REGISTRATION_SUCCES, "User created successfully!");
        }
    }
}
