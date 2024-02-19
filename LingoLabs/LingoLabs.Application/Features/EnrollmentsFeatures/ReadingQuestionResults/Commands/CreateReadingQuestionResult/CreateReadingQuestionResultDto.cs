namespace LingoLabs.Application.Features.EnrollmentsFeatures.ReadingQuestionResults.Commands.CreateReadingQuestionResult
{
    public class CreateReadingQuestionResultDto
    {
        public Guid QuestionResultId { get; set; }
        public Guid? QuestionId { get; set; }
        public Guid? LessonResultId { get; set; }
        public bool? IsCorrect { get; set; }
        public byte[]? AudioData { get; set; }
    }
}
