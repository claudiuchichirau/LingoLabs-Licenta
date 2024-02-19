using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.CreateLessonResult
{
    public class CreateLessonResultCommandResponse: BaseResponse
    {
        public CreateLessonResultCommandResponse() : base()
        {
        }

        public CreateLessonResultDto LessonResult { get; set; }
    }
}
