using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Queries;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Queries
{
    public class LanguageCompetenceResultDto
    {
        public Guid LanguageCompetenceResultId { get; set; }
        public Guid LanguageCompetenceId { get; set; }
        public Guid EnrollmentId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
