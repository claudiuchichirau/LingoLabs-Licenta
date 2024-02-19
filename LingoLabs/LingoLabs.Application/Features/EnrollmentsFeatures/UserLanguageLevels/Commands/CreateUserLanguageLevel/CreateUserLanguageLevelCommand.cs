using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.CreateUserLanguageLevel
{
    public class CreateUserLanguageLevelCommand: IRequest<CreateUserLanguageLevelCommandResponse>
    {
        public Guid EnrollmentId { get; set; }
        public Guid LanguageCompetenceId { get; set; }
        public Guid LanguageLevelId { get; set; }
    }
}
