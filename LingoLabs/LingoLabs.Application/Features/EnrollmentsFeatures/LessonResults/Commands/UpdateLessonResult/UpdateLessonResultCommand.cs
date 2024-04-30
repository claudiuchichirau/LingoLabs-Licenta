using LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Queries;
using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.UpdateLessonResult
{
    public class UpdateLessonResultCommand: IRequest<UpdateLessonResultCommandResponse>
    {
        public Guid LessonResultId { get; set; }
        public bool IsCompleted { get; set; }
        public List<QuestionResultDto> QuestionResults { get; set; } = new List<QuestionResultDto>();
    }
}
