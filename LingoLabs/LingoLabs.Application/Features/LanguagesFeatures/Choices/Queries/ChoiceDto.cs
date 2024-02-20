namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries
{
    public class ChoiceDto
    {
        public Guid ChoiceId { get; set; }
        public string ChoiceContent { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
    }
}
