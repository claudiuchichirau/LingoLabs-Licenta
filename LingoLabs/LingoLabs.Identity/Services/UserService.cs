using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Application.Models.Identity;
using LingoLabs.Domain.Common;
using LingoLabs.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace LingoLabs.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILoginService _loginService;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILoginService loginService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _loginService = loginService;
        }

        public async Task<Result<UserInfoModel>> GetCurrentUserInfoAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Result<UserInfoModel>.Failure($"User with the id:{userId} was not found!");
            }

            return Result<UserInfoModel>.Success(
                    new UserInfoModel(
                        user.UserName,
                        user.Email,
                        user.PhoneNumber,
                        user.FirstName,
                        user.LastName
                    )
                );
        }

        public async Task<(int status, string message)> ApproveAdmin(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (UserAuthenticationStatus.USER_NOT_FOUND, "User not found");
            }

            var roles = await userManager.GetRolesAsync(user);
            if (!roles.Contains("AdminPending"))
            {
                return (UserAuthenticationStatus.APPROVAL_FAIL, "User is not in the AdminPending role");
            }

            await userManager.RemoveFromRoleAsync(user, "AdminPending");

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            await userManager.AddToRoleAsync(user, "Admin");

            return (UserAuthenticationStatus.REGISTRATION_SUCCES, "User approved successfully and added to Admin role");
        }

        public async Task<Result<List<UserInfoModel>>> GetPendingAdmins()
        {
            var users = await userManager.GetUsersInRoleAsync("AdminPending");

            if (users == null || !users.Any())
            {
                return Result<List<UserInfoModel>>.Failure("No users found with the AdminPending role");
            }

            var userInfoList = new List<UserInfoModel>();

            foreach (var user in users)
            {
                var userInfoResult = await GetCurrentUserInfoAsync(user.Id);
                if (userInfoResult.IsSuccess)
                {
                    userInfoList.Add(userInfoResult.Value);
                }
            }

            return Result<List<UserInfoModel>>.Success(userInfoList);
        }

        public async Task<Result<string>> DeleteCurrentUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Result<string>.Failure($"User with the id:{userId} was not found!");
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return Result<string>.Failure("Failed to delete user");
            }

            await _loginService.Logout();

            return Result<string>.Success("User deleted successfully");
        }
    }
}
