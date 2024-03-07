using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.DeleteLessonResult
{
    public class DeleteLessonResultCommand: IRequest<DeleteLessonResultCommandResponse>
    {
        public Guid LessonResultId { get; set; }
    }
}
