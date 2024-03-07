using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.DeleteQuestionResult
{
    public class DeleteQuestionResultCommand: IRequest<DeleteQuestionResultCommandResponse>
    {
        public Guid QuestionResultId { get; set; }
    }
}
