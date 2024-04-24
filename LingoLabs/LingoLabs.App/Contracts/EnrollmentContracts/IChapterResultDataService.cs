using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface IChapterResultDataService
    {
        Task<ApiResponse<ChapterResultViewModel>> CreateChapterResultAsync(ChapterResultViewModel createChapterResultViewModel);
        Task<ApiResponse<ChapterResultViewModel>> UpdateChapterResultAsync(ChapterResultViewModel updateChapterResultViewModel);
        Task<ApiResponse<ChapterResultViewModel>> DeleteChapterResultAsync(Guid chapterResultId);
        Task<ChapterResultViewModel> GetChapterResultByIdAsync(Guid chapterResultId);
    }
}
