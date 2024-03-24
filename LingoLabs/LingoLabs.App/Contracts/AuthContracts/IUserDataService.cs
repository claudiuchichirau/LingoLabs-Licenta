using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.AuthenticationModels;

namespace LingoLabs.App.Contracts.AuthContracts
{
    public interface IUserDataService
    {
        Task<ApiResponse<UserDto>> GetUserInfoAsync();
    }
}
