namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Queries
{
    public class QuestionResultDto
    {
        public Guid QuestionResultId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid LessonResultId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
