using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandResponse: BaseResponse
    {
        public CreateLessonCommandResponse() : base()
        {
        }

        public CreateLessonDto Lesson { get; set; }
    }
}
