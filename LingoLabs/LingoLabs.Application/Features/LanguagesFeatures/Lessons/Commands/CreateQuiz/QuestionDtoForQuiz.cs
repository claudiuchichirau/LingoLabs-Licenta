using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz
{
    public class QuestionDtoForQuiz
    {
        public string QuestionRequirement { get; set; } = string.Empty;
        public QuestionType QuestionType { get; set; }
        public List<ChoiceDtoForQuiz> Choices { get; set; } = [];
    }
}
