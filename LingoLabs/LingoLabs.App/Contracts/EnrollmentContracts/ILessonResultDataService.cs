using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;
using LingoLabs.App.ViewModel.Responses;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface ILessonResultDataService
    {
        Task<ApiResponse<LessonResultResponse>> CreateLessonResultAsync(LessonResultViewModel createLessonResultViewModel);
        Task<ApiResponse<LessonResultViewModel>> UpdateLessonResultAsync(LessonResultViewModel updateLessonResultViewModel);
        Task<ApiResponse<LessonResultViewModel>> DeleteLessonResultAsync(Guid lessonResultId);
        Task<LessonResultViewModel> GetLessonResultByIdAsync(Guid lessonResultId);
    }
}
