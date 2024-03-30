namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.CreateLessonResult
{
    public class CreateLessonResultDto
    {
        public Guid LessonResultId { get; set; }
        public Guid? LessonId { get; set; }
        public Guid? ChapterResultId { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
