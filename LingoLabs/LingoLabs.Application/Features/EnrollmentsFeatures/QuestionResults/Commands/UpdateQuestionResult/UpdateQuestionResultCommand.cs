using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.UpdateQuestionResult
{
    public class UpdateQuestionResultCommand: IRequest<UpdateQuestionResultCommandResponse>
    {
        public Guid QuestionResultId { get; set; }
        public UpdateQuestionResultDto UpdateQuestionResultDto { get; set; } = new UpdateQuestionResultDto();
    }
}
