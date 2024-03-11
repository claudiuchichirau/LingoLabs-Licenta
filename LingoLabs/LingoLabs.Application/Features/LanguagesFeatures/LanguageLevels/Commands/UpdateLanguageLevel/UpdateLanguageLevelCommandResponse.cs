using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.UpdateLanguageLevel
{
    public class UpdateLanguageLevelCommandResponse: BaseResponse
    {
        public UpdateLanguageLevelDto? UpdateLanguageLevel { get; set; }
    }
}
