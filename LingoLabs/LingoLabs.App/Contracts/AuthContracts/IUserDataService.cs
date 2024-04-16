using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.AuthenticationModels;

namespace LingoLabs.App.Contracts.AuthContracts
{
    public interface IUserDataService
    {
        Task<ApiResponse<UserDto>> GetUserInfoAsync();
        Task<List<UserDto>> GetPendingAdminsAsync();
        Task<ApiResponse<UserDto>> ApproveAdminAsync(Guid userId);
        Task<ApiResponse<UserDto>> RejectAdminAsync(Guid userId);
        Task<ApiResponse<UserDto>> DeleteCurrentUserAsync();
        Task<ApiResponse<UserDto>> UpdateUserInfoAsync(UserDto user);
        Task<ApiResponse<UserDto>> ChangeUserPasswordAsync(ChangePasswordViewModel changePassword);
    }
}
