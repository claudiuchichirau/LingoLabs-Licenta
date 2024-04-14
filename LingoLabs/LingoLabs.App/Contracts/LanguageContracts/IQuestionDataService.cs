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
        Task<ApiResponse<QuestionViewModel>> CreateLanguageAsync(QuestionViewModel createQuestionViewModel);
        Task<ApiResponse<QuestionViewModel>> UpdateLanguageAsync(QuestionViewModel createQuestionViewModel);
        Task<ApiResponse<QuestionViewModel>> DeleteLanguageAsync(Guid questionId);
    }
}
