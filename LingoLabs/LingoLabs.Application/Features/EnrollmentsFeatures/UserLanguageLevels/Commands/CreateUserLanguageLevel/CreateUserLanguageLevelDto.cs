namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.CreateUserLanguageLevel
{
    public class CreateUserLanguageLevelDto
    {
        public Guid UserLanguageLevelId { get; set; }
        public Guid EnrollmentId { get; set; }
        public Guid LanguageCompetenceId { get; set; }
        public Guid LanguageLevelId { get; set; }
    }
}
