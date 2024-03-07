using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.UpdateChapterResult
{
    public class UpdateChapterResultCommand: IRequest<UpdateChapterResultCommandResponse>
    {
        public Guid ChapterResultId { get; set; }
        public UpdateChapterResultDto UpdateChapterResultDto { get; set; } = new UpdateChapterResultDto();
    }
}
