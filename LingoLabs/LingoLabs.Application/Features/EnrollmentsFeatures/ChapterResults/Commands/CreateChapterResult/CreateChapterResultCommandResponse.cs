using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.CreateChapterResult
{
    public class CreateChapterResultCommandResponse: BaseResponse
    {
        public CreateChapterResultCommandResponse() : base()
        {
        }

        public CreateChapterResultDto ChapterResult { get; set; }
    }
}
