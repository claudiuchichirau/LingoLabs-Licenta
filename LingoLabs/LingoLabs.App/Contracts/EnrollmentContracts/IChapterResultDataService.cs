using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;
using LingoLabs.App.ViewModel.Responses;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface IChapterResultDataService
    {
        Task<ApiResponse<ChapterResultResponse>> CreateChapterResultAsync(ChapterResultViewModel createChapterResultViewModel);
        Task<ApiResponse<ChapterResultViewModel>> UpdateChapterResultAsync(ChapterResultViewModel updateChapterResultViewModel);
        Task<ApiResponse<ChapterResultViewModel>> DeleteChapterResultAsync(Guid chapterResultId);
        Task<ChapterResultViewModel> GetChapterResultByIdAsync(Guid chapterResultId);
    }
}
