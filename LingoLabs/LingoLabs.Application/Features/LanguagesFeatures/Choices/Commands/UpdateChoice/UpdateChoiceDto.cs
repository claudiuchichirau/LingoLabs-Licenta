namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.UpdateChoice
{
    public class UpdateChoiceDto
    {
        public string ChoiceContent { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
