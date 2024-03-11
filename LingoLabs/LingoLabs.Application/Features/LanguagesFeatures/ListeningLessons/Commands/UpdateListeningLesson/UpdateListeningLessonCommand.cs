using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.UpdateListeningLesson
{
    public class UpdateListeningLessonCommand: IRequest<UpdateListeningLessonCommandResponse>
    {
        public Guid LessonId { get; set; }
        public UpdateListeningLessonDto UpdateListeningLessonDto { get; set; } = new UpdateListeningLessonDto();
    }
}
