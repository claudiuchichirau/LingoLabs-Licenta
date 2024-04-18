using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;

namespace LingoLabs.App.Contracts.LanguageContracts
{
    public interface IChapterDataService
    {
        Task<List<ChapterViewModel>> GetAllChaptersAsync();
        Task<ChapterViewModel> GetChapterByIdAsync(Guid chapterId);
        Task<ApiResponse<ChapterViewModel>> CreateChapterAsync(ChapterViewModel createChapterViewModel);
        Task<ApiResponse<ChapterViewModel>> UpdateChapterAsync(ChapterViewModel updateChapterViewModel);
        Task<ApiResponse<ChapterViewModel>> DeleteChapterAsync(Guid chapterId);
    }
}
