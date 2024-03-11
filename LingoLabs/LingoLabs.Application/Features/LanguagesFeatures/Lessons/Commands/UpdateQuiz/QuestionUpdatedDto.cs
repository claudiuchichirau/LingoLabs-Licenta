namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateQuiz
{
    public class QuestionUpdatedDto
    {
        public Guid QuestionId { get; set; }
        public List<ChoiceUpdatedDto> Choices { get; set; } = [];
    }
}
