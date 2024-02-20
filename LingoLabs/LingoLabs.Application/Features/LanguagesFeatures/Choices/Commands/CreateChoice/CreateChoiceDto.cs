namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.CreateChoice
{
    public class CreateChoiceDto
    {
        public Guid ChoiceId { get; set; }
        public string? ChoiceContent { get; set; }
        public bool? IsCorrect { get; set; }
        public Guid? QuestionId { get; set; }
    }
}
