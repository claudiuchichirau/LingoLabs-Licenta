using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;
using LingoLabs.App.ViewModel.LanguageModels.LanguagePlacementTest;
using LingoLabs.App.ViewModel.Responses;

namespace LingoLabs.App.Contracts.AuthContracts
{
    public interface ILanguageDataService
    {
        Task<List<LanguageViewModel>> GetAllLanguagesAsync();
        Task<LanguageViewModel> GetLanguageByIdAsync(Guid languageId);
        Task<ApiResponse<LanguageViewModel>> CreateLanguageAsync(LanguageViewModel createLanguageViewModel);
        Task<ApiResponse<LanguageViewModel>> UpdateLanguageAsync(LanguageViewModel updateLanguageViewModel);
        Task<ApiResponse<LanguageViewModel>> DeleteLanguageAsync(Guid languageId);
        Task<ApiResponse<PlacementTestViewModel>> CreatePlacementTestAsync(PlacementTestViewModel createPlacementTestViewModel);
        Task<ApiResponse<PlacementTestViewModel>> UpdatePlacementTestAsync(PlacementTestViewModel updatePlacementTestViewModel);
        Task<ApiResponse<PlacementTestViewModel>> DeletePlacementTestAsync(Guid languageId);
    }
}
