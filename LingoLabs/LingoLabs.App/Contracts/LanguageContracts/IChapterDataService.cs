using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;

namespace LingoLabs.App.Contracts.LanguageContracts
{
    public interface IChapterDataService
    {
        Task<List<ChoicerViewModel>> GetAllChaptersAsync();
        Task<ChoicerViewModel> GetChapterByIdAsync(Guid chapterId);
        Task<ApiResponse<ChoicerViewModel>> CreateChapterAsync(ChoicerViewModel createChapterViewModel);
        Task<ApiResponse<ChoicerViewModel>> UpdateChapterAsync(ChoicerViewModel updateChapterViewModel);
        Task<ApiResponse<ChoicerViewModel>> DeleteChapterAsync(Guid chapterId);
    }
}
