using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.UpdateLessonResult
{
    public class UpdateLessonResultCommandResponse: BaseResponse
    {
        public UpdateLessonResultDto? UpdateLessonResult { get; set; }
    }
}
