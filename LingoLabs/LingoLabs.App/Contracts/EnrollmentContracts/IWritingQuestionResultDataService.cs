using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface IWritingQuestionResultDataService : IQuestionResultDataService
    {
        Task<ApiResponse<WritingQuestionResultViewModel>> CreateWritingQuestionAsync(WritingQuestionResultViewModel createWritingQuestionViewModel);
        Task<WritingQuestionResultViewModel> GetWritingQuestionByIdAsync(Guid writingQuestionId);
    }
}
