using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;
using LingoLabs.App.ViewModel.Responses;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface ILanguageLevelResultDataService
    {
        Task<ApiResponse<LanguageLevelResultResponse>> CreateLanguageLevelResultAsync(LanguageLevelResultViewModel createLanguageLevelResultViewModel);
        Task<ApiResponse<LanguageLevelResultViewModel>> UpdateLanguageLevelResultAsync(LanguageLevelResultViewModel updateLanguageLevelResultViewModel);
        Task<ApiResponse<LanguageLevelResultViewModel>> DeleteLanguageLevelResultAsync(Guid languageLevelResultId);
        Task<LanguageLevelResultViewModel> GetLanguageLevelResultByIdAsync(Guid languageLevelResultId);
    }
}
