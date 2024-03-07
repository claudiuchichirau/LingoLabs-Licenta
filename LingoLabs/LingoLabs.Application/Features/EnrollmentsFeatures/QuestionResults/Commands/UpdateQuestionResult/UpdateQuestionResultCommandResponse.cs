using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.UpdateQuestionResult
{
    public class UpdateQuestionResultCommandResponse: BaseResponse
    {
        public UpdateQuestionResultDto? UpdateQuestionResult { get; set; }
    }
}
