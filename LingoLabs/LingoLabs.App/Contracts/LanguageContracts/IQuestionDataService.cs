using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;

namespace LingoLabs.App.Contracts.LanguageContracts
{
    public interface IQuestionDataService
    {
        Task<List<QuestionViewModel>> GetAllQuestionsAsync();
        Task<List<QuestionViewModel>> GetAllQuestionsByLanguageCompetenceId(Guid languageCompetenceId);
        Task<List<QuestionViewModel>> GetAllQuestionsByLanguageCompetenceIdAndLevelId(Guid languageCompetenceId, Guid languageLevelId);
        Task<List<QuestionViewModel>> GetAllQuestionsByLanguageLevelId(Guid languageLevelId);
        Task<QuestionViewModel> GetQuestionByIdAsync(Guid questionId);
        Task<ApiResponse<QuestionViewModel>> CreateQuestionAsync(QuestionViewModel createQuestionViewModel);
        Task<ApiResponse<QuestionViewModel>> UpdateQuestionAsync(QuestionViewModel createQuestionViewModel);
        Task<ApiResponse<QuestionViewModel>> DeleteQuestionAsync(Guid questionId);
    }
}
