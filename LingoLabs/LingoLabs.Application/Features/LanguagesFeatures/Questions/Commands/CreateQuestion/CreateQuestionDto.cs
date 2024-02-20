using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.CreateQuestion
{
    public class CreateQuestionDto
    {
        public Guid QuestionId { get; set; }
        public string? QuestionRequirement { get; set; }
        public LearningType? QuestionLearningType { get; set; }
        public Guid? LessonId { get; set; }
    }
}
