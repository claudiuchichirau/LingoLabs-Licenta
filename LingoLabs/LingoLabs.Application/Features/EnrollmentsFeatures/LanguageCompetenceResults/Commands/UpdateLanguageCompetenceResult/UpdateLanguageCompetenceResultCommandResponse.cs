using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.UpdateLanguageCompetenceResult
{
    public class UpdateLanguageCompetenceResultCommandResponse: BaseResponse
    {
        public UpdateLanguageCompetenceResultDto? UpdateLanguageCompetenceResult { get; set; }
    }
}
