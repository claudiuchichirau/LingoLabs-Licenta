using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.UpdateChapterResult
{
    public class UpdateChapterResultCommandResponse: BaseResponse
    {
        public UpdateChapterResultDto? UpdateChapterResult { get; set; }
    }
}
