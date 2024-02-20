using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Queries
{
    public class MatchingWordsQuestionDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionRequirement { get; set; } = string.Empty;
        public LearningType QuestionLearningType { get; set; }
        public Guid LessonId { get; set; }
    }
}
