using LingoLabs.Domain.Entities.Enrollments;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.CreateChapterResult
{
    public class CreateChapterResultDto
    {
        public Guid ChapterResultId { get; set; }
        public Guid? ChapterId { get; set; }
        public Guid? LanguageLevelResultId { get; set; }
    }
}
