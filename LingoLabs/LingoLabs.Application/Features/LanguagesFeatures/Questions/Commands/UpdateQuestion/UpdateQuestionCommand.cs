using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommand: IRequest<UpdateQuestionCommandResponse>
    {
        public Guid QuestionId { get; set; }
        public UpdateQuestionDto UpdateQuestionDto { get; set; } = new UpdateQuestionDto();
    }
}
