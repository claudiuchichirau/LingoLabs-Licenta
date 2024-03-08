using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommand: IRequest<UpdateLessonCommandResponse>
    {
        public Guid LessonId { get; set; }
        public UpdateLessonDto UpdateLessonDto { get; set; } = new UpdateLessonDto();
    }
}
