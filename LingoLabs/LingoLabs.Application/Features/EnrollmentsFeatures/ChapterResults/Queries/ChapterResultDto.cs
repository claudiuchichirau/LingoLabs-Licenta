namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Queries
{
    public class ChapterResultDto
    {
        public Guid ChapterResultId { get; set; }
        public Guid ChapterId { get; set; }
        public Guid LanguageLevelResultId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
