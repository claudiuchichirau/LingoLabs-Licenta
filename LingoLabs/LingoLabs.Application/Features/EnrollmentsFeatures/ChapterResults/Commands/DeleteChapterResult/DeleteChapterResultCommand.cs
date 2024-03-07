using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.DeleteChapterResult
{
    public class DeleteChapterResultCommand: IRequest<DeleteChapterResultCommandResponse>
    {
        public Guid ChapterResultId { get; set; }
    }
}
