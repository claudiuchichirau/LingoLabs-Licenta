using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommand: IRequest<UpdateQuestionCommandResponse>
    {
        public Guid QuestionId { get; set; }
        public string QuestionRequirement { get; set; } = string.Empty;
        public string? QuestionImageData { get; set; } = string.Empty;
        public string? QuestionVideoLink { get; set; } = string.Empty;
        public int? QuestionPriorityNumber { get; set; }
        public Guid LanguageId { get; set; }
    }
}
