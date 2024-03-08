using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommand: IRequest<DeleteLessonCommandResponse>
    {
        public Guid LessonId { get; set; }
    }
}
