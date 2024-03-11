using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.DeleteQuiz
{
    public class DeleteQuizCommand: IRequest<DeleteQuizCommandResponse>
    {
        public Guid LessonId { get; set; }
    }
}
