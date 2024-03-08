namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz
{
    public class QuestionCreatedDto
    {
        public Guid QuestionId { get; set; }
        public List<ChoiceCreatedDto> Choices { get; set; } = [];
    }
}