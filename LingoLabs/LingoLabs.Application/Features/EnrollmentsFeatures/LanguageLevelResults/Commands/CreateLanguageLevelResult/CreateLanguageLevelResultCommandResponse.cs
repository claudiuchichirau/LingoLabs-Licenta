using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.CreateLanguageLevelResult
{
    public class CreateLanguageLevelResultCommandResponse: BaseResponse
    {
        public CreateLanguageLevelResultCommandResponse() : base()
        {
        }

        public CreateLanguageLevelResultDto LanguageLevelResult { get; set; }
    }
}
