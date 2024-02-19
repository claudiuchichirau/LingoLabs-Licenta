using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.CreateUserLanguageLevel
{
    public class CreateUserLanguageLevelCommandResponse: BaseResponse
    {
        public CreateUserLanguageLevelCommandResponse() : base()
        {
        }

        public CreateUserLanguageLevelDto UserLanguageLevel { get; set; }
    }
}
