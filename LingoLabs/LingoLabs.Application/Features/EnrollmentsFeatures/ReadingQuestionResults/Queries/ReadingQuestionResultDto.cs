namespace LingoLabs.Application.Features.EnrollmentsFeatures.ReadingQuestionResults.Queries
{
    public class ReadingQuestionResultDto
    {
        public Guid QuestionResultId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid LessonResultId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
