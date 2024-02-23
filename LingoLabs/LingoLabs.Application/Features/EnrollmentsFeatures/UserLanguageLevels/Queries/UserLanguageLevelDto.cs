namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Queries
{
    public class UserLanguageLevelDto
    {
        public Guid UserLanguageLevelId { get; set; }
        public Guid EnrollmentId { get; set; }
        public Guid LanguageCompetenceId { get; set; }
        public Guid LanguageLevelId { get; set; }
    }
}
