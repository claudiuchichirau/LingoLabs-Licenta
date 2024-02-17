using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.CreateLanguageLevel
{
    public class CreateLanguageLevelCommandResponse: BaseResponse
    {
        public CreateLanguageLevelCommandResponse() : base()
        {
        }

        public CreateLanguageLevelDto LanguageLevel { get; set; }
    }
}
