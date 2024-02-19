using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ReadingQuestionResults.Commands.CreateReadingQuestionResult
{
    public class CreateReadingQuestionResultCommandResponse: BaseResponse
    {
        public CreateReadingQuestionResultCommandResponse() : base()
        {
        }

        public CreateReadingQuestionResultDto ReadingQuestionResult { get; set; }
    }
}
