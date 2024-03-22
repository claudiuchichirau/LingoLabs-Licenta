using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels.LanguageViewModels;

namespace LingoLabs.App.Contracts
{
    public interface ILanguageDataService
    {
        Task<List<LanguageViewModel>> GetAllLanguagesAsync();
        Task<LanguageDto> GetLanguageByIdAsync(Guid languageId);
        Task<ApiResponse<LanguageDto>> CreateLanguageAsync(LanguageViewModel createLanguageViewModel);
        Task<ApiResponse<LanguageDto>> UpdateLanguageAsync(LanguageViewModel updateLanguageViewModel);
        Task<ApiResponse<LanguageDto>> DeleteLanguageAsync(Guid languageId);
    }
}
