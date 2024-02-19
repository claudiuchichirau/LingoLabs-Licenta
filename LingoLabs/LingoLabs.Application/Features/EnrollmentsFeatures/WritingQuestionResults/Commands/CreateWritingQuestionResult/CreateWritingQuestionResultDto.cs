namespace LingoLabs.Application.Features.EnrollmentsFeatures.WritingQuestionResults.Commands.CreateWritingQuestionResult
{
    public class CreateWritingQuestionResultDto
    {
        public Guid QuestionResultId { get; set; }
        public Guid? QuestionId { get; set; }
        public Guid? LessonResultId { get; set; }
        public bool? IsCorrect { get; set; }
        public byte[]? ImageData { get; set; }
        public string? RecognizedText { get; set; }
    }
}
