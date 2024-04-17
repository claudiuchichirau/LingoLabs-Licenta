using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.UpdateUserLanguageLevel
{
    public class UpdateUserLanguageLevelCommand: IRequest<UpdateUserLanguageLevelCommandResponse>
    {
        public Guid UserLanguageLevelId { get; set; }
        public Guid LanguageLevelId { get; set; }
    }
}
