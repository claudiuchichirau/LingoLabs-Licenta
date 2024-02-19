using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.CreateQuestionResult
{
    public class CreateQuestionResultCommandResponse: BaseResponse
    {
        public CreateQuestionResultCommandResponse(): base()
        {
        }

        public CreateQuestionResultDto QuestionResult { get; set; }
    }
}
