using LingoLabs.Domain.Entities;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand: IRequest<CreateQuestionCommandResponse>
    {
        public string QuestionRequirement { get; set; } = default!;
        public LearningType QuestionLearningType { get; set; }
    }
}
