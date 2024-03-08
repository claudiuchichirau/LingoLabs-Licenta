using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz
{
    public class QuestionDtoForQuiz
    {
        public string QuestionRequirement { get; set; } = string.Empty;
        public LearningType QuestionLearningType { get; set; }
        public Guid LessonId { get; set; }
        public List<ChoiceDtoForQuiz> Choices { get; set; } = [];
    }
}
