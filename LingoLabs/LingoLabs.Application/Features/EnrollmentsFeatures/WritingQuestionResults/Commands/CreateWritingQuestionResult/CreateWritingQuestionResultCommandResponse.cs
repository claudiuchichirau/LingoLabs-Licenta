using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.WritingQuestionResults.Commands.CreateWritingQuestionResult
{
    public class CreateWritingQuestionResultCommandResponse: BaseResponse
    {
        public CreateWritingQuestionResultCommandResponse() : base()
        {
        }

        public CreateWritingQuestionResultDto WritingQuestionResult { get; set; }
    }
}
