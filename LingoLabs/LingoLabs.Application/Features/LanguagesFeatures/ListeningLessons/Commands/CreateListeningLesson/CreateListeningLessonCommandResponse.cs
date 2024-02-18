using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.CreateListeningLesson
{
    public class CreateListeningLessonCommandResponse: BaseResponse
    {
        public CreateListeningLessonCommandResponse() : base()
        {
        }
        
        public CreateListeningLessonDto ListeningLesson { get; set; }
    }
}
