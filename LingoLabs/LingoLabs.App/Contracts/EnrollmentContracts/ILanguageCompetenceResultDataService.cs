using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface ILanguageCompetenceResultDataService
    {
        Task<ApiResponse<LanguageCompetenceResultViewModel>> CreateLanguageCompetenceResultAsync(LanguageCompetenceResultViewModel createLanguageCompetenceResultViewModel);
        Task<ApiResponse<LanguageCompetenceResultViewModel>> UpdateLanguageCompetenceResultAsync(LanguageCompetenceResultViewModel updateLanguageCompetenceResultViewModel);
        Task<ApiResponse<LanguageCompetenceResultViewModel>> DeleteLanguageCompetenceResultAsync(Guid languageCompetenceResultId);
        Task<LanguageCompetenceResultViewModel> GetLanguageCompetenceResultByIdAsync(Guid languageCompetenceResultId);
    }
}
