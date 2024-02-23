namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Queries
{
    public class LanguageLevelResultDto
    {
        public Guid LanguageLevelResultId { get; set; }
        public Guid LanguageLevelId { get; set; }
        public Guid EnrollmentId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
