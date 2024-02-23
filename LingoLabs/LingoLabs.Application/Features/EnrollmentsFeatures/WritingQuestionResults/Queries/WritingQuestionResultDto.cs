namespace LingoLabs.Application.Features.EnrollmentsFeatures.WritingQuestionResults.Queries
{
    public class WritingQuestionResultDto
    {
        public Guid QuestionResultId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid LessonResultId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
