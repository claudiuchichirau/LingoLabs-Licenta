using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;

namespace LingoLabs.App.Contracts.EnrollmentContracts
{
    public interface IReadingQuestionResultDataService : IQuestionResultDataService
    {
        Task<ApiResponse<ReadingQuestionResultViewModel>> CreateReadingQuestionAsync(ReadingQuestionResultViewModel createReadingQuestionViewModel);
        Task<ReadingQuestionResultViewModel> GetReadingQuestionByIdAsync(Guid readingQuestionId);
    }
}
