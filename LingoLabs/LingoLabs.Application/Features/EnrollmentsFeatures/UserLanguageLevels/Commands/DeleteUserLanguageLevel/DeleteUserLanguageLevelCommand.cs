using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.DeleteUserLanguageLevel
{
    public class DeleteUserLanguageLevelCommand: IRequest<DeleteUserLanguageLevelCommandResponse>
    {
        public Guid UserLanguageLevelId { get; set; }
    }
}
