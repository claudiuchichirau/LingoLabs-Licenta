using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries
{
    public class QuestionDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionRequirement { get; set; } = string.Empty;
        public QuestionType QuestionType { get; set; }
        public Guid LessonId { get; set; }
        public int? QuestionPriorityNumber { get; set; }
    }
}
