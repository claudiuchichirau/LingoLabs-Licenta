using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface IQuestionResultDataService
    {
        Task<ApiResponse<QuestionResultViewModel>> CreateQuestionResultAsync(QuestionResultViewModel createQuestionResultViewModel);
        Task<ApiResponse<QuestionResultViewModel>> UpdateQuestionResultAsync(QuestionResultViewModel updateQuestionResultViewModel);
        Task<ApiResponse<QuestionResultViewModel>> DeleteQuestionResultAsync(Guid questionResultId);
        Task<QuestionResultViewModel> GetQuestionResultByIdAsync(Guid questionResultId);
    }
}
