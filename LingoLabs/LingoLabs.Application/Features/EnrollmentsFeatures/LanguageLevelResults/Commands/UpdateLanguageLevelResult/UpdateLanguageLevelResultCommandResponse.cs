using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.UpdateLanguageLevelResult
{
    public class UpdateLanguageLevelResultCommandResponse: BaseResponse
    { 
        public UpdateLanguageLevelResultDto? UpdateLanguageLevelResultDto { get; set; }
    }
}
