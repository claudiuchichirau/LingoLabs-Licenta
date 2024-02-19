using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.CreateLanguageCompetenceResult
{
    public class CreateLanguageCompetenceResultCommandResponse: BaseResponse
    {
        public CreateLanguageCompetenceResultCommandResponse() : base()
        {
        }

        public CreateLanguageCompetenceResultDto LanguageCompetenceResult { get; set; }
    }
}
