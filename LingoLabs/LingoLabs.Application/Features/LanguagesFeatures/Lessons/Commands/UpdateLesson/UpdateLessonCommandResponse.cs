using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandResponse: BaseResponse
    {
        public UpdateLessonDto? UpdateLesson { get; set; } 
    }
}
