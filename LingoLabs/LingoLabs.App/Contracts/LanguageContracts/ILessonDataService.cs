using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;
using LingoLabs.App.ViewModel.LanguageModels.LessonQuiz;

namespace LingoLabs.App.Contracts.LanguageContracts
{
    public interface ILessonDataService
    {
        Task<List<LessonViewModel>> GetAllLessonsAsync();
        Task<LessonViewModel> GetLessonByIdAsync(Guid lessonId);
        Task<ApiResponse<LessonViewModel>> CreateLessonAsync(LessonViewModel createLessonViewModel);
        Task<ApiResponse<LessonViewModel>> UpdateLessonAsync(LessonViewModel updateLessonViewModel);
        Task<ApiResponse<LessonViewModel>> DeleteLessonAsync(Guid lessonId);
        Task<ApiResponse<QuizViewModel>> CreateLessonQuizAsync (QuizViewModel createQuizViewModel);
        Task<ApiResponse<QuizViewModel>> UpdateLessonQuizAsync (QuizViewModel updateQuizViewModel);
        Task<ApiResponse<QuizViewModel>> DeleteLessonQuizAsync (Guid lessonId);
    }
}
