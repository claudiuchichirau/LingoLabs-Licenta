using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand: IRequest<CreateQuestionCommandResponse>
    {
        public string QuestionRequirement { get; set; } = default!;
        public QuestionType QuestionType { get; set; }
        public Guid LessonId { get; set; }
    }
}
