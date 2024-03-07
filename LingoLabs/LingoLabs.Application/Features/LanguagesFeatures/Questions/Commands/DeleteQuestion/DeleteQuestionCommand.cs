using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommand: IRequest<DeleteQuestionCommandResponse>
    {
        public Guid QuestionId { get; set; }
    }
}
