using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.UpdateUserLanguageLevel
{
    public class UpdateUserLanguageLevelCommandResponse : BaseResponse
    {
        public UpdateUserLanguageLevelDto? UserLanguageLevel { get; set; }
    }
}
