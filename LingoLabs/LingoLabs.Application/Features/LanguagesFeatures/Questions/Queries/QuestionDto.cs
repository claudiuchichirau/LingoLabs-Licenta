using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries
{
    public class QuestionDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionRequirement { get; set; } = string.Empty;
        public LearningType QuestionLearningType { get; set; }
        public Guid LessonId { get; set; }
    }
}
