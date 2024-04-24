using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface ILessonResultDataService
    {
        Task<ApiResponse<LessonResultViewModel>> CreateLessonResultAsync(LessonResultViewModel createLessonResultViewModel);
        Task<ApiResponse<LessonResultViewModel>> UpdateLessonResultAsync(LessonResultViewModel updateLessonResultViewModel);
        Task<ApiResponse<LessonResultViewModel>> DeleteLessonResultAsync(Guid lessonResultId);
        Task<LessonResultViewModel> GetLessonResultByIdAsync(Guid lessonResultId);
    }
}
