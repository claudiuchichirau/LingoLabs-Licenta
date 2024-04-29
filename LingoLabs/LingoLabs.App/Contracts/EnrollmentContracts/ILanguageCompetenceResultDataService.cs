using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;
using LingoLabs.App.ViewModel.Responses;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface ILanguageCompetenceResultDataService
    {
        Task<ApiResponse<LanguageCompetenceResultResponse>> CreateLanguageCompetenceResultAsync(LanguageCompetenceResultViewModel createLanguageCompetenceResultViewModel);
        Task<ApiResponse<LanguageCompetenceResultViewModel>> UpdateLanguageCompetenceResultAsync(LanguageCompetenceResultViewModel updateLanguageCompetenceResultViewModel);
        Task<ApiResponse<LanguageCompetenceResultViewModel>> DeleteLanguageCompetenceResultAsync(Guid languageCompetenceResultId);
        Task<LanguageCompetenceResultViewModel> GetLanguageCompetenceResultByIdAsync(Guid languageCompetenceResultId);
    }
}
