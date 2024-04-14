using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;

namespace LingoLabs.App.Contracts.LanguageContracts
{
    public interface ILanguageLevelDataService
    {
        Task<List<LanguageLevelViewModel>> GetAllLanguageLevelsAsync();
        Task<LanguageLevelViewModel> GetLanguageLevelByIdAsync(Guid languageLevelId);
        Task<ApiResponse<LanguageLevelViewModel>> CreateLanguageLevelAsync(LanguageLevelViewModel createLanguageLevelViewModel);
        Task<ApiResponse<LanguageLevelViewModel>> UpdateLanguageLevelAsync(LanguageLevelViewModel updateLanguageLevelViewModel);
        Task<ApiResponse<LanguageLevelViewModel>> DeleteLanguageLevelAsync(Guid languageLevelId);
    }
}
