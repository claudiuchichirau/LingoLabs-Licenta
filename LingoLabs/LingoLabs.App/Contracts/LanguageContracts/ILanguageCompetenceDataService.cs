using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;

namespace LingoLabs.App.Contracts.LanguageContracts
{
    public interface ILanguageCompetenceDataService
    {
        Task<List<LanguageCompetenceViewModel>> GetAllLanguageCompetencesAsync();
        Task<LanguageCompetenceViewModel> GetLanguageCompetenceByIdAsync(Guid languageCompetenceId);
        Task<ApiResponse<LanguageCompetenceViewModel>> CreateLanguageCompetenceAsync(LanguageCompetenceViewModel createLanguageCompetenceViewModel);
        Task<ApiResponse<LanguageCompetenceViewModel>> UpdateLanguageCompetenceAsync(LanguageCompetenceViewModel updateLanguageCompetenceViewModel);
        Task<ApiResponse<LanguageCompetenceViewModel>> DeleteLanguageCompetenceAsync(Guid languageCompetenceId);
    }
}
