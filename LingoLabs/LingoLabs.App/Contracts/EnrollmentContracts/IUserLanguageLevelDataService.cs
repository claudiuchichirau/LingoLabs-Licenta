using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels.EnrollmentModels;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface IUserLanguageLevelDataService
    {
        Task<ApiResponse<UserLanguageLevelViewModel>> CreateUserLanguageLevelAsync(UserLanguageLevelViewModel createUserLanguageLevelViewModel);
        Task<ApiResponse<UserLanguageLevelViewModel>> UpdateUserLanguageLevelAsync(UserLanguageLevelViewModel updateUserLanguageLevelViewModel);
        Task<ApiResponse<UserLanguageLevelViewModel>> DeleteUserLanguageLevelAsync(Guid userLanguageLevelId);
        Task<UserLanguageLevelViewModel> GetUserLanguageLevelByIdAsync(Guid userLanguageLevelId);
    }
}
