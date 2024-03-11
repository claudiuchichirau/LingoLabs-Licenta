using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.UpdateListeningLesson
{
    public class UpdateListeningLessonCommandResponse: BaseResponse
    {
        public UpdateListeningLessonDto? UpdateListeningLesson { get; set; }
    }
}
