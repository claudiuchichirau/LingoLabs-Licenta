using LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries;
using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionDto
    {
        public string QuestionRequirement { get; set; } = string.Empty;
        public string? QuestionImageData { get; set; } = string.Empty;
        public string? QuestionVideoLink { get; set; } = string.Empty;
        public int? QuestionPriorityNumber { get; set; }
        public Guid LanguageId { get; set; }
    }
}
