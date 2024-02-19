namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.CreateQuestionResult
{
    public class CreateQuestionResultDto
    {
        public Guid QuestionResultId { get; set; }
        public Guid? QuestionId { get; set; }
        public Guid? LessonResultId { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
