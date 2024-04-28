using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Commands.DeleteEnrollment;
using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.DeleteUserLanguageLevel;
using LingoLabs.Application.Models.Identity;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Domain.Common;
using LingoLabs.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading;

namespace LingoLabs.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILoginService loginService;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;
        private readonly DeleteEnrollmentCommandHandler deleteEnrollmentCommandHandler;
        private readonly DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler;
        private readonly CancellationToken cancellationToken = new CancellationToken();

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILoginService loginService, IEnrollmentRepository enrollmentRepository, IUserLanguageLevelRepository userLanguageLevelRepository, DeleteEnrollmentCommandHandler deleteEnrollmentCommandHandler, DeleteUserLanguageLevelCommandHandler deleteUserLanguageLevelCommandHandler)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.loginService = loginService;
            this.enrollmentRepository = enrollmentRepository;
            this.userLanguageLevelRepository = userLanguageLevelRepository;
            this.deleteEnrollmentCommandHandler = deleteEnrollmentCommandHandler;
            this.deleteUserLanguageLevelCommandHandler = deleteUserLanguageLevelCommandHandler;
        }

        public async Task<Result<UserInfoModel>> GetCurrentUserInfoAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Result<UserInfoModel>.Failure($"User with the id:{userId} was not found!");
            }

            Guid userIdGuid = Guid.Parse(userId);

            return Result<UserInfoModel>.Success(
                new UserInfoModel(
                    userIdGuid,
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

            var userLanguageLevels = await userLanguageLevelRepository.GetUserLanguageLevelsByUserIdAsync(Guid.Parse(userId));

            foreach (var userLanguageLevel in userLanguageLevels.Value)
            {
                var deleteUserLanguageLevelCommand = new DeleteUserLanguageLevelCommand { UserLanguageLevelId = userLanguageLevel.UserLanguageLevelId };
                var deleteUserLanguageLevelCommandResponse = await deleteUserLanguageLevelCommandHandler.Handle(deleteUserLanguageLevelCommand, cancellationToken);

                if (!deleteUserLanguageLevelCommandResponse.Success)
                {
                    return Result<string>.Failure("Failed to delete user language level");
                }
            }

            var enrollments = await enrollmentRepository.GetEnrollmentsByUserIdAsync(Guid.Parse(userId));

            foreach (var enrollment in enrollments.Value)
            {
                var deleteEnrollmentCommand = new DeleteEnrollmentCommand { EnrollmentId = enrollment.EnrollmentId };
                var deleteEnrollmentCommandResponse = await deleteEnrollmentCommandHandler.Handle(deleteEnrollmentCommand, cancellationToken);

                if (!deleteEnrollmentCommandResponse.Success)
                {
                    return Result<string>.Failure("Failed to delete enrollment");
                }
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

        public async Task<(int status, string message)> RejectAdmin(string userId)
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

            if (!await roleManager.RoleExistsAsync("Student"))
            {
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }

            await userManager.AddToRoleAsync(user, "Student");

            return (UserAuthenticationStatus.REGISTRATION_SUCCES, "User rejected successfully and added to Student role");
        }
        
        public async Task<(bool success, string message)> UpdateUserInfoAsync(string userId, UserInfoModel model)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return (false, "Failed to update user info.");
            }

            return (true, "User info updated successfully.");
        }
    }
}
