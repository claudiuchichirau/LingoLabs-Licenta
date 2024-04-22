using LingoLabs.Domain.Entities;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateQuiz
{
    public class UpdateQuestionForDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionRequirement { get; set; } = string.Empty;
        public QuestionType QuestionType { get; set; }
        public string? QuestionImageData { get; set; } = string.Empty;
        public string? QuestionVideoLink { get; set; } = string.Empty;
        public int? QuestionPriorityNumber { get; set; }
        public List<UpdateChoiceForDto> Choices { get; set; } = [];
        public Guid? LanguageId { get; set; }
    }
}
