namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz
{
    public class ChoiceDtoForQuiz
    {
        public string ChoiceContent { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
    }
}