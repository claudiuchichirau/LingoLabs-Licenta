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
        private readonly ILoginService loginService;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILoginService loginService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            loginService = loginService;
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

            await loginService.Logout();

            return Result<string>.Success("User deleted successfully");
        }

        public async Task<(bool success, string message)> ChangePassword(string userId, ChangePasswordModel model)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            if (model.CurrentPassword == null || model.NewPassword == null)
            {
                return (false, "Password fields cannot be null.");
            }

            if (model.CurrentPassword == model.NewPassword)
            {
                return (false, "New password cannot be the same as the old password.");
            }

            var checkOldPassword = await userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!checkOldPassword)
            {
                return (false, "The old password is incorrect.");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                return (false, "Failed to change password.");
            }

            return (true, "Password changed successfully.");
        }

    }
}
