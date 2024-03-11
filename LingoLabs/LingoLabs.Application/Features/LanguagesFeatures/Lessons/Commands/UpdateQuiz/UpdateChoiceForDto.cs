namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateQuiz
{
    public class UpdateChoiceForDto
    {
        public Guid ChoiceId { get; set; }
        public string ChoiceContent { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
